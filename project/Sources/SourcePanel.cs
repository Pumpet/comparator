using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Common;

namespace Sources
{
  /* Widgets for different data sources */
  public partial class SourcePanel : UserControl, IViewSource
  {
    //--- IViewSource members
    public IView ParentView { get; set; }
    public ISqlView SqlView { get; set; }
    public event Func<string, object, object> Command;
    //--- For excel operations
    List<Tuple<string, string>> openedBooks = new List<Tuple<string, string>>(); // workbook name, path
    SynchronizationContext sync;
    [DllImport("user32.dll")]
    static extern bool IsWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int showWindowCommand);
    [DllImport("user32.dll")]
    static extern bool IsIconic(IntPtr hWnd);
    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);
    bool canConnectToSheet = true;
    Excel.Worksheet currWs;
    Excel.Worksheet CurrWs
    {
      get { return currWs; }
      set
      {
        if (currWs != null)
        {
          currWs.SelectionChange -= SheetSelectionChange;
          ExcelProc.ReleaseCom(ref currWs);
        }
        if (value != null)
        {
          currWs = value;
          ((Excel._Worksheet)currWs).Activate();
          currWs.SelectionChange += SheetSelectionChange;
        }
      }
    }
    //~~~~~ Common ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    public SourcePanel()
    {
      InitializeComponent();
    }
    //-------------------------------------------------------------------------
    private void SourcePanel_Load(object sender, EventArgs e)
    {
      ParentView = (IView)FindForm();
      sync = SynchronizationContext.Current;
      panelCommon.Dock = DockStyle.Top;
      panelCommon.BringToFront();
      
      panelDatabase.Dock = DockStyle.Fill;
      panelDatabase.Visible = false;
      panelDatabase.BringToFront(); 
      
      panelExcel.Dock = DockStyle.Fill;
      panelExcel.Visible = false;
      panelExcel.BringToFront();

      panelCsv.Dock = DockStyle.Fill;
      panelCsv.Visible = false;
      panelCsv.BringToFront();

      panelXml.Dock = DockStyle.Fill;
      panelXml.Visible = false;
      panelXml.BringToFront();        

      Dock = DockStyle.Fill;
    }
    //-------------------------------------------------------------------------
    public void Free()
    {
      CurrWs = null;
    }
    //-------------------------------------------------------------------------
    void OnError(Exception ex)
    {
      if (ParentView is ILoger)
        ((ILoger)ParentView).Error(tbSrcName.Text, ex);
      else
        MessageBox.Show(tbSrcName.Text + "\n" + ex.Message);
    }
    //-------------------------------------------------------------------------
    private void cboxSource_SelectedIndexChanged(object sender, EventArgs e)
    {
      string s = (sender as ComboBox).SelectedItem.ToString();
      panelDatabase.Visible = (s == "Database");
      panelExcel.Visible = (s == "Excel");
      panelCsv.Visible = (s == "CSV");
      panelXml.Visible = (s == "XML");
      (s == "Database" ? panelDatabase : (s == "Excel" ? panelExcel : (s == "CSV" ? panelCsv : panelXml))).BringToFront();
    }
    //-------------------------------------------------------------------------
    private void bViewData_Click(object sender, EventArgs e)
    {
      if (Command != null)
      {
        Cursor = Cursors.WaitCursor;
        object res = null;
        if ((sender as Control).Name == "bViewDataCsv")
          res = Command("ViewDataCsv", null);
        if ((sender as Control).Name == "bViewDataXls")
          res = Command("ViewDataExcel", null);
        if (res == null)
        {
          Cursor = Cursors.Default;
          return;
        }
        FormFlatData f = new FormFlatData((res as Tuple<object, string, string>).Item1, (res as Tuple<object, string, string>).Item3);
        f.Text = (res as Tuple<object, string, string>).Item2;
        Cursor = Cursors.Default;
        f.ShowDialog((Form)ParentView);
      }
    }
    #endregion
    //~~~~~ DB ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    private void bSetConn_Click(object sender, EventArgs e)
    {
      if (SqlView is Form)
      {
        ((Form)SqlView).Text = Parent.Text;
        ((Form)SqlView).ShowDialog((Form)ParentView);
      }
    }
    #endregion
    //~~~~~ Csv ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    private void bSelectFileCsv_Click(object sender, EventArgs e)
    {
      if (Command != null)
        Command("SelectFileCsv", null);
    }
    //-------------------------------------------------------------------------
    private void tbNameCsv_TextChanged(object sender, EventArgs e)
    {
      ttip.SetToolTip(tbNameCsv, tbNameCsv.Text);
    }
    #endregion
    //~~~~~ Excel ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    private void tbNameXls_TextChanged(object sender, EventArgs e)
    {
      ttip.SetToolTip(tbNameXls, tbNameXls.Text);
    }
    //-------------------------------------------------------------------------
    private void bSelectFileXls_Click(object sender, EventArgs e)
    {
      ddbOpened.ShowDropDown();
    }
    //-------------------------------------------------------------------------
    /* List of opened workbooks */
    public void ddbOpened_DropDownOpening(object sender, EventArgs e)
    {
      openedBooks.Clear();
      ddbOpened.DropDownItems.Clear();
      ddbOpened.DropDownItems.Add("From file...", Properties.Resources.selectfile, SelectExcelFromFile);
      try
      {
        List<Tuple<string, string>> wbooks = ExcelProc.GetOpenedXlsNames();
        if (wbooks.Count > 0)
        {
          ddbOpened.DropDownItems.Add(new ToolStripSeparator());
          for (int i = 0; i < wbooks.Count; i++)
          {
            openedBooks.Add(wbooks[i]);
            ddbOpened.DropDownItems.Add(wbooks[i].Item1 + (string.IsNullOrEmpty(wbooks[i].Item2) ? "" : " (" + wbooks[i].Item2 + ")"));
          }
        }
      }
      catch (Exception ex)
      {
        OnError(ex);
      }
    }
    //-------------------------------------------------------------------------
    /* Get workbook from file */
    private void SelectExcelFromFile(object sender, EventArgs e)
    {
      if (Command == null || !(bool)Command("SelectFileExcel", null))
        return;
      ((Form)ParentView).Cursor = Cursors.WaitCursor;
      try
      {
        CurrWs = null;
        Excel.Workbook wb = ExcelProc.GetWorkbook(Path.GetFileName(tbNameXls.Text), Path.GetDirectoryName(tbNameXls.Text), false, false);
        FillSheetsList(wb);
        if (wb != null && !wb.Application.Visible)
          wb.Application.Quit();
        ExcelProc.ReleaseCom(ref wb);
      }
      catch (Exception ex)
      {
        OnError(ex);
      }
      finally
      {
        ((Form)ParentView).Cursor = Cursors.Default;
      }
    }
    //-------------------------------------------------------------------------
    /* Get workbook from opened workbook  */
    private void ddbOpened_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      int idx = ddbOpened.DropDownItems.IndexOf(e.ClickedItem) - 2;
      if (idx < 0) return;
      try
      {
        if (string.IsNullOrEmpty(openedBooks[idx].Item2))
          tbNameXls.Text = openedBooks[idx].Item1;
        else
          tbNameXls.Text = Path.Combine(openedBooks[idx].Item2, openedBooks[idx].Item1);
        CurrWs = null;
        Excel.Workbook wb = ExcelProc.GetWorkbook(openedBooks[idx].Item1, openedBooks[idx].Item2, true, true);
        //ShowWorkbook(wb);
        SetForegroundWindow((ParentView as Form).Handle);
        FillSheetsList(wb);
        CurrWs = ExcelProc.GetSheet(wb, cboxSheets.Text);
        ExcelProc.ReleaseCom(ref wb);
      }
      catch (Exception ex)
      {
        OnError(ex);
      }
    }
    //-------------------------------------------------------------------------
    /* Open workbook */
    private void bOpenXls_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(tbNameXls.Text)) return;
      try
      {
        CurrWs = null;
        Excel.Workbook wb = ExcelProc.GetWorkbook(Path.GetFileName(tbNameXls.Text), Path.GetDirectoryName(tbNameXls.Text), true, true);
        ShowWorkbook(wb);
        FillSheetsList(wb);
        CurrWs = ExcelProc.GetSheet(wb, cboxSheets.Text);
        ExcelProc.ReleaseCom(ref wb);
      }
      catch (Exception ex)
      {
        OnError(ex);
      }
    }
    //-------------------------------------------------------------------------
    /* Change sheet */
    private void cboxSheets_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (!canConnectToSheet) return;
      try
      {
        CurrWs = null;
        Excel.Workbook wb = ExcelProc.GetOpenedWorkbook(Path.GetFileName(tbNameXls.Text), Path.GetDirectoryName(tbNameXls.Text));
        if (wb != null)
        {
          CurrWs = ExcelProc.GetSheet(wb, cboxSheets.Text);
          ExcelProc.ReleaseCom(ref wb);
        }
      }
      catch (Exception ex)
      {
        OnError(ex);
      }
    }
    //-------------------------------------------------------------------------
    /* Fill sheets for opened workbook */
    void FillSheetsList(Excel.Workbook wb)
    {
      if (wb != null && Command != null)
      {
        canConnectToSheet = false;
        try
        {
          Command("SetExcelSeets", Tuple.Create(
            wb.Worksheets.OfType<Excel.Worksheet>().Select(x => x.Name).ToList(),
            ((Excel.Worksheet)wb.ActiveSheet).Name));
        }
        finally
        {
          canConnectToSheet = true;
        }
      }
    }
    //-------------------------------------------------------------------------
    /* Show selected workbook */
    void ShowWorkbook(Excel.Workbook wb)
    {
      if (wb != null)
      {
        IntPtr hwnd = (IntPtr)wb.Application.Hwnd;
        if (hwnd != IntPtr.Zero && IsWindow(hwnd))
        {
          ShowWindow(hwnd, IsIconic(hwnd) ? 9 : 5);
          SetForegroundWindow(hwnd);
        }
      }
    }
    //-------------------------------------------------------------------------
    /* Handle change selection in sheet */
    void SheetSelectionChange(Excel.Range Target)
    {
      sync.Send(SetAddress, Target.Areas[1].get_Address(false, false, Excel.XlReferenceStyle.xlA1, false, false));
    }
    void SetAddress(object addr)
    {
      tbRngStart.Focus();
      if (string.IsNullOrEmpty((string)addr) || !((string)addr).Contains(':')) return;
      string[] se = ((string)addr).Split(':');
      tbRngStart.Text = se[0];
      tbRngEnd.Text = se[1];
    }
    #endregion
    //~~~~~ Xml ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void bXmlOptions_Click(object sender, EventArgs e)
    {
      if (Command != null)
      {
        Cursor = Cursors.WaitCursor;
        object res = Command("RunXmlOptionsPrepare", null);
        if (res == null)
        {
          Cursor = Cursors.Default;
          return;
        }
        FormXml form = XmlController.GetForm((res as Tuple<object, string>).Item1);
        form.Text = (res as Tuple<object, string>).Item2;
        Cursor = Cursors.Default;
        form.ShowDialog((Form) ParentView);
        //Cursor = Cursors.WaitCursor;
        //Command("GetXmlFields", null);
        //Cursor = Cursors.Default;
      }
    }
  }
}

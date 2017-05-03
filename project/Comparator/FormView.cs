using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Common;
using Sources;

namespace Comparator
{
  public partial class FormView : Form, IView, ILoger
  {
    Dictionary<string, BindingSource> bs = new Dictionary<string, BindingSource>(); // инициализация - в InitBindings(), установка - в SetData()
    Dictionary<string, string> propNames; // инициализация - в SetDataProps(), использование для привязки - в SetBindings()
    SourcePanel panelSourceA = new SourcePanel();
    SourcePanel panelSourceB = new SourcePanel();
    FormCompare formCompare;
    bool checkChanges;
    //-------------------------------------------------------------------------
    [DllImport("user32.dll")]
    static extern bool IsWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int showWindowCommand);
    //~~~~~~~~ IView Members ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    public List<string> RecentFiles
    {
      set 
      { 
        bLoad.DropDownItems.Clear();
        bLoad.DropDownItems.Add("Profile from file...", null, bLoad_Click);
        if (value.Count > 0) 
          bLoad.DropDownItems.Add(new ToolStripSeparator());
        foreach (string s in value)
          bLoad.DropDownItems.Add(s, null, bLoad_Click);
      }
    }
    public IViewSource viewSourceA { get { return panelSourceA; } }
    public IViewSource viewSourceB { get { return panelSourceB; } }
    public bool Compared { set { bResult.Enabled = value; } }
    public IntPtr ResultHWnd { get; set; }
    public event Action<bool> NewProfile;
    public event Action<string> LoadProfile;
    public event Func<bool> SaveProfile;
    public event Func<bool> Check;
    public event Action<TaskContext, TaskContext, TaskContext> Compare;
    public event Action CompareStop;
    public event Action Result;
    public event Action DataEdit;
    public event Func<bool> CloseView;
    public event Action<bool, bool, bool, List<string>, List<string>> FillColPairs;
    public event Action<int[]> RemoveColPair;
    public event Func<int[], int, int> MoveColPair;
    public event Func<List<string>, List<string>, bool> GetFields;
    //-------------------------------------------------------------------------
    /* установка имен полей объектов с данными для последующей привязки в SetBindings() */
    public void SetDataProps(Dictionary<string, string> names)
    {
      propNames = names ?? new Dictionary<string, string>();
    }
    //-------------------------------------------------------------------------
    /* прием объектов с данными в соответствующие BindingSources */
    public void SetData(Dictionary<string, object> bIn)
    {
      checkChanges = false;
      foreach (var b in bs)
        if (bIn.ContainsKey(b.Key))
        {
          b.Value.DataSource = bIn[b.Key];
          b.Value.ResetBindings(true);
        }
      RefreshControls();
      checkChanges = true;
    }
    //-------------------------------------------------------------------------
    /* обновление для указанных BindingSources */
    public void RefreshData(Dictionary<string, object> bIn, params string[] bsNames)
    {
      if (bsNames != null)
      {
        foreach (var n in bsNames)
          if (bs.ContainsKey(n))
          {
            if (bIn != null && bIn.ContainsKey(n))
              bs[n].DataSource = bIn[n];
            bs[n].ResetBindings(true);
          }
      }
      else
        foreach (var b in bs)
        {
          if (bIn != null && bIn.ContainsKey(b.Key))
            b.Value.DataSource = bIn[b.Key];
          b.Value.ResetBindings(true);
        }
      RefreshControls();
    }
    //-------------------------------------------------------------------------
    /*  необходимые обновления контролов после обновления данных */
    void RefreshControls()
    {
      panelSourceA.cboxSheets.Enabled = (panelSourceA.cboxSheets.Items.Count > 0);
      panelSourceB.cboxSheets.Enabled = (panelSourceB.cboxSheets.Items.Count > 0);
    }
    //-------------------------------------------------------------------------
    public void WaitResult(bool wait, string msg)
    {
      Cursor = wait ? Cursors.WaitCursor : Cursors.Default;
      if (formCompare != null)
        formCompare.WaitState(wait, msg);
    }
    //-------------------------------------------------------------------------
    public string LoadFile(string folder, string filter, string ext)
    {
      OpenFileDialog dlg = new OpenFileDialog();
      dlg.Filter = filter;  
      dlg.DefaultExt = ext; 
      dlg.FilterIndex = 1;
      dlg.InitialDirectory = folder;
      DialogResult res = dlg.ShowDialog();
      Refresh();
      if (res != DialogResult.OK)
        return null;
      else
        return dlg.FileName;
    }
    //-------------------------------------------------------------------------
    public string SaveFile(string folder, string name, string filter, string ext)
    {
      SaveFileDialog dlg = new SaveFileDialog();
      dlg.Filter = filter;  
      dlg.DefaultExt = ext; 
      dlg.FilterIndex = 1;
      dlg.OverwritePrompt = true;
      dlg.FileName = name;
      dlg.InitialDirectory = folder;
      if (dlg.ShowDialog() != DialogResult.OK)
        return null;
      else
        return dlg.FileName;
    }
    //-------------------------------------------------------------------------
    public string SaveRequest()
    {
      return (MessageBox.Show("Save current Profile?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question).ToString());
    }
    #endregion
    //~~~~~~~~ ILoger Members ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    public void Error(string mess, object objErr = null)
    {
      if (objErr != null)
        mess = string.Format("{0}\n{1}", mess, (objErr is Exception) ? ((Exception)objErr).Message : objErr.ToString());
      MessageBox.Show(mess, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    //-------------------------------------------------------------------------
    public void Message(string mess, bool critical = false)
    {
      lblMsg.Text = mess;
      lblMsg.ForeColor = critical ? Color.Red : SystemColors.ControlText;
    }
    #endregion
    //~~~~~~~~ Form ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region 
    //-------------------------------------------------------------------------
    public FormView()
    {
      InitializeComponent();
      ttip.SetToolTip(bColSelect,"Select fields for compare (F4)");
      ttip.SetToolTip(bUp, "Move selected rows up (Ctrl+Up)");
      ttip.SetToolTip(bDown, "Move selected rows down (Ctrl+Down)");
      ttip.SetToolTip(bColDel, "Delete selected rows (Del)");
      ttip.SetToolTip(panelSourceA.bSetConn, "Set connection and query (Alt+A)");
      ttip.SetToolTip(panelSourceB.bSetConn, "Set connection and query (Alt+B)");
      InitBindings();
    }
    private void bPin_Click(object sender, EventArgs e)
    {
      TopMost = !TopMost;
      bPin.Image = TopMost ? Properties.Resources.pinon : Properties.Resources.pinoff;
    }
    //-------------------------------------------------------------------------
    private void FormView_Load(object sender, EventArgs e)
    {
      panelSourceA.Parent = gbSourceA;
      panelSourceB.Parent = gbSourceB;
      SetBindings();
      foreach (var b in bs.Values)
        b.ListChanged += OnDataChanged;
    }
    //-------------------------------------------------------------------------
    private void OnDataChanged(object sender, ListChangedEventArgs e)
    {
      if (checkChanges && e.ListChangedType != ListChangedType.Reset)
      {
        if (Text.Last() != '*') Text = Text + "*";
        if (DataEdit != null) DataEdit();
      }
    }
    //-------------------------------------------------------------------------
    private void FormView_FormClosing(object sender, FormClosingEventArgs e)
    {
      EndEdit();
      if (CloseView != null)
        e.Cancel = !CloseView();
      if (!e.Cancel)
      {
        panelSourceA.Free();
        panelSourceB.Free();
      }
    }
    //-------------------------------------------------------------------------
    private void FormView_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F2 && e.Modifiers == Keys.None)
        bSave.PerformClick();
      if (e.KeyCode == Keys.F3 && e.Modifiers == Keys.None)
      {
        toolTop.Focus();
        bLoad.ShowDropDown();
      }
      if (e.KeyCode == Keys.F3 && e.Modifiers == Keys.Shift)
      {
        toolTop.Focus();
        bNew.ShowDropDown();
      }
      if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.None)
        bColSelect.PerformClick();
      if (e.KeyCode == Keys.F5 && e.Modifiers == Keys.None)
        bRun.PerformClick();
      if (e.KeyCode == Keys.F5 && e.Modifiers == Keys.Shift)
        bCheck.PerformClick();
      if (e.KeyCode == Keys.F6 && e.Modifiers == Keys.None)
        bResult.PerformClick();
      if (e.KeyCode == Keys.A && e.Modifiers == Keys.Alt && panelSourceA.panelDatabase.Visible)
        panelSourceA.bSetConn.PerformClick();
      if (e.KeyCode == Keys.B && e.Modifiers == Keys.Alt && panelSourceB.panelDatabase.Visible)
        panelSourceB.bSetConn.PerformClick();
    }
    #endregion
    //~~~~~~~~ Commands ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region 
    //-------------------------------------------------------------------------
    private void bNew_Click(object sender, EventArgs e)
    {
      EndEdit();
      if (NewProfile != null) NewProfile((sender as ToolStripMenuItem).Name == "bNewCopy");
    }
    private void bSave_Click(object sender, EventArgs e)
    {
      EndEdit();
      if (SaveProfile != null) SaveProfile();
    }
    private void bLoad_Click(object sender, EventArgs e)
    {
      EndEdit(); 
      if (LoadProfile != null) LoadProfile(((ToolStripMenuItem)sender).Text);
    }
    private void bCheck_Click(object sender, EventArgs e)
    {
      EndEdit();
      if (Check != null)
      {
        try
        {
          Cursor = Cursors.WaitCursor;
          Check();
        }
        finally
        {
          Cursor = Cursors.Default;
        }
      }
    }
    //-------------------------------------------------------------------------
    private void bRun_Click(object sender, EventArgs e)
    {
      EndEdit();
      formCompare = new FormCompare();
      formCompare.Start += (a, b, c) => { if (Compare != null) Compare(a, b, c); };
      formCompare.Stop += () => { if (CompareStop != null) CompareStop(); };
      formCompare.ShowDialog(this);
      formCompare.Dispose();
      formCompare = null;
      checkChanges = false;
      bs["DbSourceA"].ResetBindings(true);
      bs["DbSourceB"].ResetBindings(true);
      checkChanges = true;
      if (ResultHWnd != IntPtr.Zero && IsWindow(ResultHWnd))
        SetForegroundWindow(ResultHWnd);
    }
    //-------------------------------------------------------------------------
    private void bResult_Click(object sender, EventArgs e)
    {
      if (ResultHWnd != IntPtr.Zero && IsWindow(ResultHWnd))
      {
        ShowWindow(ResultHWnd, 5);
        SetForegroundWindow(ResultHWnd);
      }
      else if (Result != null) 
        Result();
    }
    //-------------------------------------------------------------------------
    private void EndEdit()
    {
      Control ctrl = ActiveControl;
      Controls[0].Focus();
      ctrl.Focus();
    }
    #endregion
    //~~~~~~~~ Controls ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    private void cbSend_CheckedChanged(object sender, EventArgs e)
    {
      bool b = (sender as CheckBox).Checked;
      tbSendTo.ReadOnly = !b;
      tbSendTo.BackColor = b ? SystemColors.Window : SystemColors.Control;
      cboxResMail.Enabled = b;
      tbSubject.ReadOnly = !b;
      tbSubject.BackColor = b ? SystemColors.Window : SystemColors.Control;
    }
    //-------------------------------------------------------------------------
    private void cbMatchInOrder_CheckedChanged(object sender, EventArgs e)
    {
      dgCols.Columns["Key"].Visible = !(sender as CheckBox).Checked;
    }
    //-------------------------------------------------------------------------
    private void cbMatchAllPairs_CheckedChanged(object sender, EventArgs e)
    {
      dgCols.Columns["Match"].Visible = !(sender as CheckBox).Checked;
    }
    #endregion
    //~~~~~~~~ Grid ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    private void dgCols_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
      DataGridView g = (DataGridView)sender;
      if (g.RowCount == 0) return;
      DataGridViewCell cell = g[e.ColumnIndex, e.RowIndex];
      string colName = g.Columns[e.ColumnIndex].Name;
      if (cell.Value == null) return;
      bool emptyCells = (g["ColsA", cell.RowIndex].Value == null || g["ColsB", cell.RowIndex].Value == null 
        || string.IsNullOrEmpty(g["ColsA", cell.RowIndex].Value.ToString()) || string.IsNullOrEmpty(g["ColsB", cell.RowIndex].Value.ToString()));
      if (colName == "Key" && !emptyCells)
      {
        if ((bool)cell.FormattedValue == false) g["Match", cell.RowIndex].Value = true;
        cell.Value = !(bool)cell.Value;
      }
      if (colName == "Match" && !emptyCells)
      {
        if ((bool)cell.FormattedValue == true) g["Key", cell.RowIndex].Value = false;
        cell.Value = !(bool)cell.Value;
      }
    }
    //-------------------------------------------------------------------------
    private void bColSelect_Click(object sender, EventArgs e)
    {
      if (GetFields != null)
      {
        List<string> listA = new List<string>(), listB = new List<string>();
        try
        {
          Cursor = Cursors.WaitCursor;
          if (GetFields(listA, listB))
          {
            FormSelectPair f = new FormSelectPair(listA, listB);
            f.Location = Point.Add(Location, new Size(tableAll.Width - f.Width, tableAll.Location.Y + 24));
            f.Height = tableAll.Height;
            f.SetPairs += SetPairs;
            Cursor = Cursors.Default;
            f.ShowDialog(this);
          }
        }
        finally
        {
          Cursor = Cursors.Default;
        }
      }
    }
    //------------------------------------------------------------------------
    /* обработчик для приема данных из формы выбора полей */
    private void SetPairs(bool clear, bool key, bool match, List<string> listA, List<string> listB)
    {
      if (FillColPairs == null) return;
      FillColPairs(clear, key, match, listA, listB);
      bs["Cols"].ResetBindings(true);
      bs["Cols"].Position = clear ? 0 : dgCols.RowCount - 1;
    }
    //-------------------------------------------------------------------------
    private void bColDel_Click(object sender, EventArgs e)
    {
      if (dgCols.RowCount == 0 || RemoveColPair == null) return;
      dgCols.EndEdit();
      int[] idx = dgCols.SelectedCells.OfType<DataGridViewCell>().Select(x => x.RowIndex).Distinct().ToArray();
      RemoveColPair(idx);
      bs["Cols"].ResetBindings(true);
      bs["Cols"].Position = idx.Min();
    }
    //-------------------------------------------------------------------------
    private void bUp_Click(object sender, EventArgs e)
    {
      MoveRow(-1);
    }
    private void bDown_Click(object sender, EventArgs e)
    {
      MoveRow(1);
    }
    private void MoveRow(int offset)
    {
      if (dgCols.RowCount == 0 || MoveColPair == null) return;
      dgCols.EndEdit();
      int[] idxs = dgCols.SelectedCells.OfType<DataGridViewCell>().Select(x => x.RowIndex).Distinct().ToArray();
      int idx = MoveColPair(idxs, offset);
      if (idx >= 0)
      {
        bs["Cols"].ResetBindings(true);
        bs["Cols"].Position = idx;
        foreach (DataGridViewCell c in dgCols.SelectedCells) c.Selected = false;
        foreach (int i in idxs) dgCols.Rows[i + offset].Selected = true;
      }
    }
    //-------------------------------------------------------------------------
    private void dgCols_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.Up)
      {
        MoveRow(-1);
        e.SuppressKeyPress = true;
      }
      if (e.Control && e.KeyCode == Keys.Down)
      {
        MoveRow(1);
        e.SuppressKeyPress = true;
      }
      if (e.KeyCode == Keys.Delete)
        bColDel.PerformClick();
      if (e.KeyCode == Keys.Insert)
        bColSelect.PerformClick();
    }
    #endregion
    //~~~~~~~~ Binding ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region 
    //-------------------------------------------------------------------------
    void InitBindings()
    {
      // порядок важен для последующего ResetBindings
      bs.Add("Profile", new BindingSource());
      bs.Add("MailResult", new BindingSource());
      bs.Add("Cols", new BindingSource());
      bs.Add("SourceA", new BindingSource());
      bs.Add("SourceB", new BindingSource());
      bs.Add("SourceTypeA", new BindingSource());
      bs.Add("SourceTypeB", new BindingSource());
      bs.Add("SheetsA", new BindingSource());
      bs.Add("SheetsB", new BindingSource());
      bs.Add("DbSourceA", new BindingSource());
      bs.Add("DbSourceB", new BindingSource());
      bs.Add("ExcelSourceA", new BindingSource());
      bs.Add("ExcelSourceB", new BindingSource());
      bs.Add("CsvSourceA", new BindingSource());
      bs.Add("CsvSourceB", new BindingSource());
      bs.Add("XmlSourceA", new BindingSource());
      bs.Add("XmlSourceB", new BindingSource());
    }
    //-------------------------------------------------------------------------
    void SetBindings()
    {
      DataSourceUpdateMode m = DataSourceUpdateMode.OnPropertyChanged;
      DataBindings.Add("Text", bs["Profile"], propNames["ViewCaption"], true, DataSourceUpdateMode.Never);
      // profile
      cbDiffOnly.DataBindings.Add("Checked", bs["Profile"], propNames["DiffOnly"], true, m);
      cbOnlyA.DataBindings.Add("Checked", bs["Profile"], propNames["OnlyA"], true, m);
      cbOnlyB.DataBindings.Add("Checked", bs["Profile"], propNames["OnlyB"], true, m);
      cbCheckRepeats.DataBindings.Add("Checked", bs["Profile"], propNames["CheckRepeats"], true, m);
      cbCaseSens.DataBindings.Add("Checked", bs["Profile"], propNames["CaseSens"], true, m);
      cbNullAsStr.DataBindings.Add("Checked", bs["Profile"], propNames["NullAsStr"], true, m);
      cbTryConvert.DataBindings.Add("Checked", bs["Profile"], propNames["TryConvert"], true, m);
      cbSend.DataBindings.Add("Checked", bs["Profile"], propNames["Send"], true, m);
      tbSendTo.DataBindings.Add("Text", bs["Profile"], propNames["SendTo"], true, m);
      rbResExcel.DataBindings.Add("Checked", bs["Profile"], propNames["ResExcel"]);
      rbResHTML.DataBindings.Add("Checked", bs["Profile"], propNames["ResHTML"]);
      tbResFolder.DataBindings.Add("Text", bs["Profile"], propNames["ResFolder"], true, m);
      tbResFile.DataBindings.Add("Text", bs["Profile"], propNames["ResFile"], true, m);
      cbTimeInResFile.DataBindings.Add("Checked", bs["Profile"], propNames["TimeInResFile"], true, m);
      cboxResMail.DataSource = bs["MailResult"];
      cboxResMail.DataBindings.Add("SelectedItem", bs["Profile"], propNames["ResMail"], true, m);
      tbSubject.DataBindings.Add("Text", bs["Profile"], propNames["Subject"], true, m);
      // source A
      panelSourceA.tbSrcName.DataBindings.Add("Text", bs["SourceA"], propNames["src.Name"], false, m);
      panelSourceA.cboxSource.DataSource = bs["SourceTypeA"];
      panelSourceA.cboxSource.DataBindings.Add("SelectedItem", bs["SourceA"], propNames["src.SrcType"], true, m);
      // source A - db
      panelSourceA.tbConnInfo.DataBindings.Add("Text", bs["DbSourceA"], propNames["src.ConnectionInfo"], true, DataSourceUpdateMode.Never);
      // source A - excel
      panelSourceA.tbNameXls.DataBindings.Add("Text", bs["ExcelSourceA"], propNames["src.File"], true, m);
      panelSourceA.cboxSheets.DataSource = bs["SheetsA"];
      panelSourceA.cboxSheets.DataBindings.Add("SelectedItem", bs["ExcelSourceA"], propNames["src.Sheet"], true, m);
      panelSourceA.tbRngStart.DataBindings.Add("Text", bs["ExcelSourceA"], propNames["src.RngStart"], true, m);
      panelSourceA.tbRngEnd.DataBindings.Add("Text", bs["ExcelSourceA"], propNames["src.RngEnd"], true, m);
      panelSourceA.cbFirstLineNamesXls.DataBindings.Add("Checked", bs["ExcelSourceA"], propNames["src.FirstLineNames"], true, m);
      // source A - csv
      panelSourceA.tbNameCsv.DataBindings.Add("Text", bs["CsvSourceA"], propNames["src.File"], true, m);
      panelSourceA.cbFirstLineNamesCsv.DataBindings.Add("Checked", bs["CsvSourceA"], propNames["src.FirstLineNames"], true, m);
      panelSourceA.tbDelimiter.DataBindings.Add("Text", bs["CsvSourceA"], propNames["src.Delimiter"], true, m);
      panelSourceA.tbCp.DataBindings.Add("Text", bs["CsvSourceA"], propNames["src.Codepage"], true, m);
      // source A - xml
      panelSourceA.tbXmlInfo.DataBindings.Add("Text", bs["XmlSourceA"], propNames["src.XmlInfo"], true, DataSourceUpdateMode.Never);
      // source B
      panelSourceB.tbSrcName.DataBindings.Add("Text", bs["SourceB"], propNames["src.Name"], false, m);
      panelSourceB.cboxSource.DataSource = bs["SourceTypeB"];
      panelSourceB.cboxSource.DataBindings.Add("SelectedItem", bs["SourceB"], propNames["src.SrcType"], true, m);
      // source B - db
      panelSourceB.tbConnInfo.DataBindings.Add("Text", bs["DbSourceB"], propNames["src.ConnectionInfo"], true, DataSourceUpdateMode.Never);
      // source B - excel
      panelSourceB.tbNameXls.DataBindings.Add("Text", bs["ExcelSourceB"], propNames["src.File"], true, m);
      panelSourceB.cboxSheets.DataSource = bs["SheetsB"];
      panelSourceB.cboxSheets.DataBindings.Add("SelectedItem", bs["ExcelSourceB"], propNames["src.Sheet"], true, m);
      panelSourceB.tbRngStart.DataBindings.Add("Text", bs["ExcelSourceB"], propNames["src.RngStart"], true, m);
      panelSourceB.tbRngEnd.DataBindings.Add("Text", bs["ExcelSourceB"], propNames["src.RngEnd"], true, m);
      panelSourceB.cbFirstLineNamesXls.DataBindings.Add("Checked", bs["ExcelSourceB"], propNames["src.FirstLineNames"], true, m);
      // source B - csv
      panelSourceB.tbNameCsv.DataBindings.Add("Text", bs["CsvSourceB"], propNames["src.File"], true, m);
      panelSourceB.cbFirstLineNamesCsv.DataBindings.Add("Checked", bs["CsvSourceB"], propNames["src.FirstLineNames"], true, m);
      panelSourceB.tbDelimiter.DataBindings.Add("Text", bs["CsvSourceB"], propNames["src.Delimiter"], true, m);
      panelSourceB.tbCp.DataBindings.Add("Text", bs["CsvSourceB"], propNames["src.Codepage"], true, m);
      // source B - xml
      panelSourceB.tbXmlInfo.DataBindings.Add("Text", bs["XmlSourceB"], propNames["src.XmlInfo"], true, DataSourceUpdateMode.Never);
      // cols
      cbMatchInOrder.DataBindings.Add("Checked", bs["Profile"], propNames["MatchInOrder"], true, m);
      cbMatchAllPairs.DataBindings.Add("Checked", bs["Profile"], propNames["MatchAllPairs"], true, m);
      dgCols.AutoGenerateColumns = false;
      dgCols.DataSource = bs["Cols"];
      dgCols.Columns["Key"].DataPropertyName = propNames["cols.Key"];
      dgCols.Columns["Match"].DataPropertyName = propNames["cols.Match"];
      dgCols.Columns["ColsA"].DataPropertyName = propNames["cols.ColA"];
      dgCols.Columns["ColsB"].DataPropertyName = propNames["cols.ColB"];
    }
    #endregion
  }
}

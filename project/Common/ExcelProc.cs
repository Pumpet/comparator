using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Common
{
  public static class ExcelProc
  {
    //-------------------------------------------------------------------------
    /* книга по имени и пути - из открытых или с диска */
    public static Excel.Workbook GetWorkbook(string name, string path, bool findOpened = true, bool visibleNew = true)
    {
      Excel.Workbook wb = null;
      string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine(path, name));
      if (findOpened || string.IsNullOrEmpty(path) || !File.Exists(file))
      {
        wb = GetOpenedWorkbook(name, path);
      }
      if (wb == null && !string.IsNullOrEmpty(path) && File.Exists(file))
      {
        Excel.Application xls = new Excel.Application {Visible = visibleNew};
        wb = xls.Workbooks.Open(file);
        ReleaseCom(ref xls);
      }
      if (wb == null)
        throw new Exception("Excel workbook \"" + file + "\" not found!");
      return wb;
    }
    //-------------------------------------------------------------------------
    /* открытая книга */
    public static Excel.Workbook GetOpenedWorkbook(string name, string path)
    {
      Excel.Workbook wb = null;
      List<Excel.Workbook> wbooks = GetOpenedXls();
      if (wbooks != null && wbooks.Count > 0)
      {
        wb = wbooks.FirstOrDefault(x => x.Name == name && x.Path == path);
        for (int i = 0; i < wbooks.Count; i++)
          if (wbooks[i] != wb)
          {
            ReleaseCom(wbooks[i]);
            wbooks[i] = null;
          }
      }
      return wb;
    }
    //-------------------------------------------------------------------------
    /* лист книги по текущему имени */
    public static Excel.Worksheet GetSheet(Excel.Workbook wb, string sheetName)
    {
      Excel.Worksheet ws = null;
      int idx = wb.Worksheets.OfType<Excel.Worksheet>().Where(x => x.Name == sheetName).Select(y => y.Index).FirstOrDefault();
      if (idx > 0)
        ws = wb.Worksheets[idx];
      return ws;
    }
    //-------------------------------------------------------------------------
    /* уборка ком, чтобы эксели не болтались в памяти */
    public static void ReleaseCom<T>(T o) where T : class
    {
      ReleaseCom(ref o);
    }
    public static void ReleaseCom<T>(ref T o) where T : class
    {
      if (o != null)
      {
        Marshal.FinalReleaseComObject(o);
        o = null;
        GC.Collect();
      }
    }
    //-------------------------------------------------------------------------
    /* список книг (имя, путь) из окон живых процессов Excel */
    public static List<Tuple<string, string>> GetOpenedXlsNames()
    {
      List<Tuple<string, string>> res = new List<Tuple<string, string>>();
      List<Excel.Workbook> wbooks = GetOpenedXls();
      if (wbooks.Count > 0)
      {
        for (int i = 0; i < wbooks.Count; i++)
        {
          res.Add(new Tuple<string, string>(wbooks[i].Name, wbooks[i].Path));
          ReleaseCom(wbooks[i]);
          wbooks[i] = null;
        }
      }
      return res;
    }
    //-------------------------------------------------------------------------
    /* список объектов книг из окон живых процессов Excel */
    public static List<Excel.Workbook> GetOpenedXls()
    {
      List<Excel.Workbook> wbooks = new List<Excel.Workbook>();
      List<Process> procs = new List<Process>();
      procs.AddRange(Process.GetProcessesByName("excel"));

      foreach (Process p in procs)
      {
        if ((int)p.MainWindowHandle > 0)
        {
          int childWindow = 0;
          var cb = new EnumChildCallback(EnumChildProc);
          EnumChildWindows((int)p.MainWindowHandle, cb, ref childWindow);

          if (childWindow > 0)
          {
            const uint OBJID_NATIVEOM = 0xFFFFFFF0;
            Guid IID_IDispatch = new Guid("{00020400-0000-0000-C000-000000000046}");
            Excel.Window window = null;
            if (AccessibleObjectFromWindow(childWindow, OBJID_NATIVEOM, IID_IDispatch.ToByteArray(), ref window) >= 0)
              wbooks.AddRange(window.Application.Workbooks.Cast<Excel.Workbook>());
          }
        }
      }
      return wbooks;
    }
    #region ---- вспомогательные процедуры для получения объектов из процессов

    // объект из окна
    [DllImport("Oleacc.dll")]
    public static extern int AccessibleObjectFromWindow(
          int hwnd, uint dwObjectID, byte[] riid,
          ref Excel.Window ptr);

    // для EnumChildProc
    public delegate bool EnumChildCallback(int hwnd, ref int lParam);

    // перечисление дочерних окон - остановка и возврат lParam - в EnumChildProc (передаваемой в lpEnumFunc)
    [DllImport("User32.dll")]
    public static extern bool EnumChildWindows(
          int hWndParent, EnumChildCallback lpEnumFunc,
          ref int lParam);

    // имя класса окна
    [DllImport("User32.dll")]
    public static extern int GetClassName(
          int hWnd, StringBuilder lpClassName, int nMaxCount);

    // функция обратного вызова для EnumChildWindows - анализирует дочернее окно, если эксель - останавливает перечисление
    public static bool EnumChildProc(int hwndChild, ref int lParam)
    {
      StringBuilder buf = new StringBuilder(128);
      GetClassName(hwndChild, buf, 128);
      if (buf.ToString() == "EXCEL7")
      {
        lParam = hwndChild;
        return false;
      }
      return true;
    }
    #endregion 
  
    //-------------------------------------------------------------------------
    /* выдача грида в эксель */
    public static void GridToExcel(DataGridView dg, int maxRows = 0)
    {
      if (dg.ColumnCount == 0) return;

      if (maxRows > 0 && dg.RowCount > maxRows) // ограничение, чтобы не подвешивать надолго
        MessageBox.Show(string.Format("Sorry, we offer only {0} rows in Excel", maxRows), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      else
        maxRows = dg.RowCount;

      object[,] data = new object[maxRows, dg.ColumnCount];

      Form f = dg.FindForm();
      f.Cursor = Cursors.WaitCursor;
      Excel.Application xlsApp = null;
      Excel.Workbook xlsWb = null;
      Excel.Worksheet ws = null;
      Excel.Range rg = null;
      try
      {
        xlsApp = new Excel.Application();
        xlsApp.Visible = false;
        xlsApp.ScreenUpdating = false;
        xlsWb = xlsApp.Workbooks.Add();
        xlsApp.Calculation = Excel.XlCalculation.xlCalculationManual;
        ws = (Excel.Worksheet)xlsWb.Worksheets.Add();
        Excel.Range cell = ws.get_Range("A1");

        //----- caps
        string[] caps = new string[dg.ColumnCount];
        for (int c = 0; c < dg.ColumnCount; c++)
          caps[dg.Columns[c].DisplayIndex] = dg.Columns[c].HeaderText;
        rg = ws.get_Range(cell, cell.get_Offset(0, dg.ColumnCount - 1));
        rg.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, caps);
        rg.Font.Bold = true;
        rg.HorizontalAlignment = Excel.Constants.xlCenter;
        cell = cell.get_Offset(1, 0);

        //----- data
        for (int r = 0; r < data.GetLength(0); r++)
          foreach (DataGridViewColumn c in dg.Columns)
          {
            if (dg.Rows[r].Cells[c.Index].Value is Guid)
              data[r, c.DisplayIndex] = dg.Rows[r].Cells[c.Index].Value.ToString();
            else
              data[r, c.DisplayIndex] = dg.Rows[r].Cells[c.Index].Value;
          }
        rg = ws.get_Range(cell, cell.get_Offset(maxRows - 1, dg.ColumnCount - 1));
        rg.set_Value(Excel.XlRangeValueDataType.xlRangeValueDefault, data);

        ws.UsedRange.Columns.AutoFit();
        xlsApp.ScreenUpdating = true;
        xlsApp.Visible = true;
      }
      catch (Exception)
      {
        if (xlsWb != null && xlsApp.Workbooks.Count > 0)
          xlsWb.Close(false);
        if (xlsApp != null) xlsApp.Quit();
        throw;
      }
      finally
      {
        ReleaseCom(ref rg);
        ReleaseCom(ref ws);
        ReleaseCom(ref xlsWb);
        ReleaseCom(ref xlsApp);
        f.Cursor = Cursors.Default;
      }
    }
  }
}

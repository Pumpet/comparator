using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;
using Common;

namespace Sources
{
  public class ExcelContent : SourceContent  // содержимое источника Excel
  {
    public string Filename { get; set; }
    public string Sheet { get; set; }
    public string RngStart { get; set; }
    public string RngEnd { get; set; }
    public bool FirstLineNames { get; set; }
    List<string> sheets = new List<string>();
    [XmlArrayItem("sheet")]
    public List<string> Sheets
    {
      get { return sheets; }
      set { sheets = value; }
    }
    //-------------------------------------------------------------------------
    public ExcelContent()
    {
      Fields = new List<string>();
    }
    //-------------------------------------------------------------------------
    public override void Check()
    {
      if (string.IsNullOrEmpty(Filename))
        throw new Exception(Parent.Name + ": file name not defined!");
    }
    //-------------------------------------------------------------------------
    public override List<string> GetCheckFields()
    {
      Check();
      GetExcelData(true);
      return Fields;
    }
    //-------------------------------------------------------------------------
    public override void GetData(TaskContext c, Action<bool> a)
    {
      base.GetData(c, a);
      Check();
      if (c != null && c.OnProgress != null)
        c.OnProgress(0, "open file \"" + Path.GetFileName(Filename) + "\" ...");
      GetExcelData();
      GetDataEnd(null, string.Format("{0} rows", Parent.DT != null ? Parent.DT.Rows.Count : 0));
    }
    //-------------------------------------------------------------------------
    /* данные из excel - в таблицу или только список имен полей */
    void GetExcelData(bool fieldsOnly = false)
    {
      Excel.Workbook wb = null;
      Excel.Worksheet ws = null;
      Excel.Range rg = null;
      try
      {
        wb = ExcelProc.GetWorkbook(Path.GetFileName(Filename), Path.GetDirectoryName(Filename), true, false);
        ws = ExcelProc.GetSheet(wb, Sheet);
        if (ws == null)
          throw new Exception(Parent.Name + ": excel sheet \"" + Sheet + "\" not exist");
        rg = GetRange(ws);
        SheetToTable(ws, rg, fieldsOnly);
        if (Parent.DT != null) Parent.DT.TableName = Parent.Name;
      }
      finally
      {
        if (wb != null && !wb.Application.Visible)
          wb.Application.Quit();
        ExcelProc.ReleaseCom(ref wb);
        ExcelProc.ReleaseCom(ref ws);
        ExcelProc.ReleaseCom(ref rg);
      }
    }
    //-------------------------------------------------------------------------
    /* диапазон по заданным границам или вся занятая область */
    Excel.Range GetRange(Excel.Worksheet ws)
    {
      Excel.Range rg;
      string rg1 = (RngStart == null ? "" : RngStart.Trim().ToUpper()), rg2 = (RngEnd == null ? "" : RngEnd.Trim().ToUpper());
      string patt = @"^[a-zA-Z]{1,2}$|^[1-9]{1}[0-9]{0,4}$|^[a-zA-Z]{1,2}[1-9]{1}[0-9]{0,4}$|^$",
        pattR = @"^[1-9]{1}[0-9]{0,4}$", pattC = @"^[a-zA-Z]{1,2}$";

      if (!Regex.IsMatch(rg1, patt) || !Regex.IsMatch(rg2, patt))
        throw new Exception(string.Format("Range \"{0}:{1}\" is not valid", rg1, rg2));
      try
      {
        int r1 = ws.UsedRange.Row,
          c1 = ws.UsedRange.Column,
          r2 = ws.UsedRange.Row + ws.UsedRange.Rows.Count - 1,
          c2 = ws.UsedRange.Column + ws.UsedRange.Columns.Count - 1;
        if (rg1 != "")
        {
          if (Regex.IsMatch(rg1, pattR)) // это строка
            r1 = ((Excel.Range)ws.Rows[rg1]).Row;
          else if (Regex.IsMatch(rg1, pattC)) // это столбец
            c1 = ((Excel.Range)ws.Columns[rg1]).Column;
          else
          { r1 = ws.get_Range(rg1).Row; c1 = ws.get_Range(rg1).Column; }
        }
        if (rg2 != "")
        {
          if (Regex.IsMatch(rg2, pattR))
            r2 = ((Excel.Range)ws.Rows[rg2]).Row;
          else if (Regex.IsMatch(rg2, pattC))
            c2 = ((Excel.Range)ws.Columns[rg2]).Column;
          else
          { r2 = ws.get_Range(rg2).Row; c2 = ws.get_Range(rg2).Column; }
        }
        rg = ws.Range[ws.Cells[r1, c1], ws.Cells[r2, c2]];
      }
      catch (Exception ex)
      {
        throw new Exception(string.Format("{0}: can't define range \"{1}:{2}\"", Parent.Name, rg1, rg2), ex);
      }
      return rg;
    }
    //-------------------------------------------------------------------------
    /* данные в Parent.DT или только имена полей в Fields из указанного диапазона листа excel */
    void SheetToTable(Excel.Worksheet ws, Excel.Range rg = null, bool fieldsOnly = false)
    {
      Fields = new List<string>();
      if (!fieldsOnly) Parent.DT = new DataTable();

      if (rg == null)
        rg = ws.UsedRange;
      if (rg.Columns.Count == 0 || rg.Rows.Count == 0)
        return;

      DataTable dt = new DataTable();

      int r0 = (FirstLineNames ? 1 : 0);
      //поля        
      for (int c = 1; c <= rg.Columns.Count; c++)
      {
        Type type = typeof(String);
        if (FirstLineNames && rg.Cells[1, c] != null
          && rg.Cells[1, c].Value != null && !string.IsNullOrEmpty(rg.Cells[1, c].Value.ToString().Trim()))
        {
          string colName = rg.Cells[1, c].Value.ToString().Trim();
          string tmpName = colName;
          int suff = 0;
          while (dt.Columns.OfType<DataColumn>().Count(x => x.ColumnName == colName) > 0)
            colName = tmpName + "_" + (++suff).ToString();
          dt.Columns.Add(colName, type);
        }
        else
          dt.Columns.Add(GetRCName((rg.Cells[1, c] as Excel.Range).get_Address(), false), type);
      }
      Fields.AddRange(dt.Columns.OfType<DataColumn>().Select(x => x.ColumnName));

      //данные
      if (!fieldsOnly)
      {
        object[,] dtArr; // для данных Range
        if (rg.Columns.Count == 1 && rg.Rows.Count == 1) // т.к.в случае одной ячейки value не возвращает массив
        {
          dtArr = (object[,])Array.CreateInstance(typeof(Object), new[] { 1, 1 }, new[] { 1, 1 });
          dtArr[1, 1] = rg.get_Value();
        }
        else
          dtArr = (object[,])rg.get_Value();

        Parent.DT = dt.Clone();
        object[] dtRow = new object[dtArr.GetLength(1)]; // для формирования строки
        for (int r = r0; r < dtArr.GetLength(0); r++)
        {
          for (int c = 0; c < dtArr.GetLength(1); c++)
            dtRow[c] = dtArr[r + 1, c + 1];
          Parent.DT.LoadDataRow(dtRow, true);
        }
      }
    }
    //-------------------------------------------------------------------------
    string GetRCName(string addr, bool isRow)
    {
      return addr.ToCharArray().Where(c => (!isRow && char.IsLetter(c)) || (isRow && char.IsDigit(c))).Aggregate("", (current, c) => current + c.ToString());
    }
  }
}

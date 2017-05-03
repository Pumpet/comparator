using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataComparer
{
  public class CompareResult : IDisposable // результат сверки
  {
    FormResult formResult;
    string defaultFileName = "result.htm";
    string defaultStylesFileName = "styles.css";
    public string NameA { get; private set; } // имя источника A
    public string NameB { get; private set; } // имя источника B
    public int RowsCntA { get; private set; } // строк в источнике A
    public int RowsCntB { get; private set; } // строк в источнике B
    public List<string>[] ColNames { get; private set; } // заголовки источников
    public DataTable DtA { get; private set; } // данные только в источнике A
    public DataTable DtB { get; private set; } // данные только в источнике B
    public DataTable DtDiff { get; private set; } // данные расхождений (попарно, 0-й столбец - номер пары, 1-й столбец - номер внутри пары (0,1))
    public DataTable DtIdent { get; private set; } // данные совпадений (попарно, 0-й столбец - номер пары, 1-й столбец - номер внутри пары (2,3))
    public Dictionary<int, List<int>> Diffs  { get; private set; } // номера столбцов расхождений для каждой пары строк с расхождениями (ключ - номер пары из dtDiff) 
    public List<int> KeyCols { get; private set; } // номера ключевых столбцов
    public List<int> MatchCols { get; private set; } // номера столбцов, которые сравнивали
    public Dictionary<int,int> RepeatRows { get; private set; } // номера повторяющихся строк (номер строки = номер пары в dtDiff|dtIdent + номер внутри пары)
    //-------------------------------------------------------------------------
    public CompareResult(string nameA, string nameB, int rowsCntA, int rowsCntB, List<string>[] colNames, 
      DataTable dtDiff, DataTable dtIdent, DataTable dtA, DataTable dtB,
      Dictionary<int, List<int>> diffs, List<int> keyCols, List<int> matchCols, Dictionary<int, int> repeatRows)
    {
      ColNames = colNames;
      NameA = nameA;
      NameB = nameB;
      RowsCntA = rowsCntA;
      RowsCntB = rowsCntB;
      DtDiff = dtDiff;
      DtIdent = dtIdent; 
      DtA = dtA; 
      DtB = dtB; 
      Diffs = diffs; 
      KeyCols = keyCols; 
      MatchCols = matchCols; 
      RepeatRows = repeatRows;
    }
    //-------------------------------------------------------------------------
    public void Dispose()
    {
      CloseForm();
      DtA = null;
      DtB = null;
      DtDiff = null;
      DtIdent = null;
      GC.Collect();
    }
    //-------------------------------------------------------------------------
    public IntPtr OpenForm(string resultPath, string resultFileName, string styleFile)
    {
      if (formResult == null || formResult.IsDisposed)
        formResult = new FormResult(this, resultPath, resultFileName, styleFile);
      formResult.Show();
      formResult.Focus();
      return formResult.Handle;
    }
    //-------------------------------------------------------------------------
    public void CloseForm()
    {
      if (formResult != null && !formResult.IsDisposed)
      {
        formResult.Close(true);
        formResult = null;
      }
    }
    //-------------------------------------------------------------------------
    public string ToHTML(bool open = false, bool inExcel = false, string filePath = null, string fileName = null, string stylesFile = null, Action<string, Exception> onError = null)
    {
      string file = "";
      try
      {
        filePath = string.IsNullOrEmpty(filePath) ? AppDomain.CurrentDomain.BaseDirectory : filePath;
        fileName = string.IsNullOrEmpty(fileName) ? Path.ChangeExtension(defaultFileName, inExcel ? "xls" : "htm") : fileName;
        file = Path.Combine(filePath, fileName);
        if (string.IsNullOrEmpty(stylesFile) || !File.Exists(stylesFile))
          stylesFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultStylesFileName);

        CreateHtmlResult(file, stylesFile);

        if (open && !inExcel)
          Process.Start(file);

        if (open && inExcel)
        {
          Excel.Application xlsApp = null;
          Excel.Workbook xlsWb = null;
          try
          {
            xlsApp = new Excel.Application {Visible = true};
            xlsWb = xlsApp.Workbooks.Open(file);
          }
          catch (Exception)
          {
            if (xlsWb != null && xlsApp.Workbooks.Count > 0)
              xlsWb.Close(SaveChanges: false);
            if (xlsApp != null) xlsApp.Quit();
            throw;
          }
          finally
          {
            if (xlsWb != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsWb);
            if (xlsApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);
            GC.Collect();
          }
        }
      }
      catch (Exception ex)
      {
        string msg = "Error send to \"" + file + "\"";
        if (onError != null)
          onError(msg, ex);
        else
          throw;
      }
      return file;
    }
    //-------------------------------------------------------------------------
    public string HtmlSummary()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("<HTML>");
      sb.AppendLine("<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=windows-1251\">");
      sb.AppendLine("<HEAD>");
      sb.AppendLine("<STYLE>");
      sb.AppendLine(@"body { font-size: 12pt; font-family: Calibri; }");
      sb.AppendLine("</STYLE>");
      sb.AppendLine("</HEAD>");
      sb.AppendLine("<BODY>");
      sb.AppendLine(string.Format("<p>Compare \"{0}\" and \"{1}\" result summary:<br/>", NameA, NameB));
      sb.AppendLine(string.Format("<br/>Total rows in \"{0}\": {1}", NameA, RowsCntA));
      sb.AppendLine(string.Format("<br/>Total rows in \"{0}\": {1}", NameB, RowsCntB));
      sb.AppendLine(string.Format("<br/>Differencies: {0} row pairs", DtDiff.Rows.Count / 2));
      if (DtIdent != null)
        sb.AppendLine(string.Format("<br/>Identicals: {0} row pairs", DtIdent.Rows.Count / 2));
      if (DtA != null)
        sb.AppendLine(string.Format("<br/>Rows only in \"{0}\": {1}", NameA, DtA.Rows.Count));
      if (DtB != null)
        sb.AppendLine(string.Format("<br/>Rows only in \"{0}\": {1}</p>", NameB, DtB.Rows.Count));
      sb.AppendLine("</BODY>");
      sb.AppendLine("</HTML>");
      return sb.ToString();
    }
    //-------------------------------------------------------------------------
    public void CreateHtmlResult(string fileName, string stylesFileName)
    {
      StringBuilder sb = new StringBuilder();
      int colCnt = DtDiff.Columns.Count-2;
      int span = colCnt + 1;
      sb.AppendLine("<HTML xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
      sb.AppendLine("<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=windows-1251\">");
      sb.AppendLine("<HEAD>");
      sb.AppendLine("<STYLE>");
      sb.AppendLine(File.Exists(stylesFileName) ? File.ReadAllText(stylesFileName) : defHtmlStyle);
      sb.AppendLine("</STYLE>");
      sb.AppendLine("</HEAD>");
      sb.AppendLine("<BODY>");
      sb.AppendLine("<TABLE class=dat>");
      // top
      sb.AppendLine(string.Format("<tr><td class=h colspan={0}>Compare \"{1}\"(A) and \"{2}\"(B)<a name=\"top\"/></td></tr>", span, NameA, NameB));
      sb.AppendLine(string.Format("<tr><td class=l colspan={0}>Rows in A: {1}</td></tr>", span, RowsCntA));
      sb.AppendLine(string.Format("<tr><td class=l colspan={0}>Rows in B: {1}</td></tr>", span, RowsCntB));
      if (DtDiff.Rows.Count == 0)
        sb.AppendLine(string.Format("<tr><td class=l colspan={0}>Differencies: {1} row pairs</td></tr>", 
          span, DtDiff.Rows.Count / 2));
      else
        sb.AppendLine(string.Format("<tr><td class=l colspan={0}><a href=\"#{1}\">Differencies: {2} row pairs</a></td></tr>",
          span, ResultType.rtDiff.ToString(), DtDiff.Rows.Count / 2));
      if (DtIdent != null)
      {
        if (DtIdent.Rows.Count == 0)
          sb.AppendLine(string.Format("<tr><td class=l colspan={0}>Identicals: {1} row pairs</td></tr>",
                span, DtIdent.Rows.Count / 2));
        else
          sb.AppendLine(string.Format("<tr><td class=l colspan={0}><a href=\"#{1}\">Identicals: {2} row pairs</a></td></tr>",
                span, ResultType.rtIdent.ToString(), DtIdent.Rows.Count / 2));
      }
      if (DtA != null)
      {
        if (DtA.Rows.Count == 0)
          sb.AppendLine(string.Format("<tr><td class=l colspan={0}>Only in A: {1} rows</td></tr>",
                span, DtA.Rows.Count));
        else
          sb.AppendLine(string.Format("<tr><td class=l colspan={0}><a href=\"#{1}\">Only in A: {2} rows</a></td></tr>",
                span, ResultType.rtA.ToString(), DtA.Rows.Count));
      }
      if (DtB != null)
      {
        if (DtB.Rows.Count == 0)
          sb.AppendLine(string.Format("<tr><td class=l colspan={0}>Only in B: {1} rows</a></td></tr>",
                colCnt, DtB.Rows.Count));
        else
          sb.AppendLine(string.Format("<tr><td class=l colspan={0}><a href=\"#{1}\">Only in B: {2} rows</a></td></tr>",
                colCnt, ResultType.rtB.ToString(), DtB.Rows.Count));
      }
      // tables
      if (DtDiff.Rows.Count != 0)
        sb.AppendLine(CreateHtmlTable(ResultType.rtDiff, DtDiff, "Differencies").ToString());
      if (DtIdent != null && DtIdent.Rows.Count != 0)
        sb.AppendLine(CreateHtmlTable(ResultType.rtIdent, DtIdent, "Identicals").ToString());
      if (DtA != null && DtA.Rows.Count != 0) 
        sb.AppendLine(CreateHtmlTable(ResultType.rtA, DtA, "Rows only in A").ToString());
      if (DtB != null && DtB.Rows.Count != 0)
        sb.AppendLine(CreateHtmlTable(ResultType.rtB, DtB, "Rows only in B").ToString());
      sb.AppendLine("</TABLE>");
      sb.AppendLine("</BODY>");
      sb.AppendLine("</HTML>");
      //
      File.WriteAllText(fileName, sb.ToString(), Encoding.GetEncoding(1251));
    }
    //-------------------------------------------------------------------------
    StringBuilder CreateHtmlTable(ResultType rt, DataTable dt, string capt)
    {
      int delta = rt == ResultType.rtDiff || rt == ResultType.rtIdent ? 2 : 0; // нужно учесть наличие служебных столбцов в начале таблиц парных данных
      int colCnt = dt.Columns.Count;
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("<tr><td class=h></tr>");
      sb.AppendLine(string.Format("<tr><td class=h colspan={0}>{1}</td></tr>", colCnt - delta + 1, capt));
      // heads
      for (int t = 0; t < 2; t++)
      {
        if (rt == ResultType.rtA && t == 1) continue;
        if (rt == ResultType.rtB && t == 0) continue;
        string st = "h1";
        if (rt == ResultType.rtA || t == 1) st = "h2";
        sb.Append("<TR class=" + st + "><TD>" + (rt == ResultType.rtDiff || rt == ResultType.rtIdent ? (t == 0 ? "A" : "B") : ""));
        for (int c = 0; c < colCnt - delta; c++)
        {
          string cls = "";
          if (KeyCols.Contains(c))
            cls = " class=k";
          if ((rt == ResultType.rtDiff || rt == ResultType.rtIdent) && MatchCols.Contains(c))
            cls = " class=m";
          if (rt == ResultType.rtDiff && Diffs.Values.SelectMany(v => v).Distinct().Contains(c))
            cls = " class=e";
          sb.Append("<TD" + cls + ">" + ColNames[t][c]
            .Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;"));
        }
        sb.AppendLine("</TR>");
      }
      // data
      for (int r = 0; r < dt.Rows.Count; r++)
      {
        int key = rt == ResultType.rtDiff || rt == ResultType.rtIdent ? (int)dt.Rows[r][0] * 10 + (int)dt.Rows[r][1] : -1;
        sb.Append("<TR" + (r%2 == 0 || rt == ResultType.rtA || rt == ResultType.rtB ? "" : " class=r") + ">");
        if ((rt == ResultType.rtDiff || rt == ResultType.rtIdent) && RepeatRows.ContainsKey(key))
        { 
          if (r > 0) 
            sb.Append("<TD class=e>R" + RepeatRows[key].ToString()); 
          else 
            sb.Append(string.Format("<TD class=e><a name=\"{0}\">R{1}</a></td>", rt.ToString(), RepeatRows[key])); 
        }
        else
        { 
          if (r > 0) 
            sb.Append("<TD>"); 
          else 
            sb.Append(string.Format("<TD><a name=\"{0}\"/></td>", rt.ToString())); 
        }
        for (int c = delta; c < colCnt; c++)
        {
          string cls = "";
          if (KeyCols.Contains(c-delta))
            cls = " class=k";
          if (rt == ResultType.rtDiff && Diffs.ContainsKey((int)dt.Rows[r][0]) && Diffs[(int)dt.Rows[r][0]].Contains(c - delta))
            cls = " class=e";
          sb.Append("<TD" + cls + ">" + dt.Rows[r][c].ToString()
            .Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;"));
        }
        sb.AppendLine("</TR>");
      }
      //
      sb.AppendLine("<tr><td class=l><a href=\"#top\">Top</a></td></tr>");
      return sb;
    }
    //-------------------------------------------------------------------------
    #region default styles instead of styles.css
    string defHtmlStyle = @"
* {
  mso-displayed-decimal-separator:""\."";
  mso-displayed-thousand-separator:"" "";
  font-size: 10pt;
  font-family: Calibri;
 }
.dat {
  border-collapse: collapse;
  border: 0;
  font-size: 10pt;
  font-weight: 400;
  font-family: Calibri;
 }
tr td {
  border: .5pt solid silver;  
  padding: 5;
 }
tr td.e { /* different data */
  border: .5pt solid silver;  
  color: DarkRed;
 }
tr td.k { /* keys */
  border: .5pt solid silver;  
  color: DarkBlue;
 }
tr td.m { /* matched data */
  border: .5pt solid silver;  
  color: DarkGreen;
 }
tr td.h { /* main headers */
  border: 0;
  font-size: 13pt;
  font-weight: 700;
  font-family: Calibri;
}
tr td.l { /* headers */
  border: 0;
  font-size: 11pt;
  font-weight: 400;
  font-family: Calibri;
}
tr.h1 td { /* column names for source A */
  border: .5pt solid silver;  
  background: #F5F5F5;
 }
tr.h2 td { /* column names for source B */
  border-bottom: 3pt double silver;  
  background: #DCDCDC;
 }
tr.r td { /* row from source B */ 
  background: #CCFFFF;
  border-bottom: 3pt double silver;  
  padding: 5;
 }
a { /* link in headers */
  font-size: 11pt;
  font-weight: 400;
}
";
    #endregion
  }
}

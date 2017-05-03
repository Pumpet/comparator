using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SqlSource;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace Comparator
{
  //public enum SourceType { Database, Excel, CSV } // виды источников данных
  //public class Source // источник данных для сверки
  //{
  //  string name;
  //  public string Name { get { return string.IsNullOrEmpty(name) ? InnerName : name; } set { name = value; } }
  //  [XmlIgnore]
  //  public string InnerName { get; set; } // "внутреннее" имя источника - по умолчанию, для сообщений

  //  SourceType srcType;
  //  public SourceType SrcType 
  //  {
  //    get { return srcType; }
  //    set
  //    {
  //      srcType = value;
  //      content = GetContent(value); // текущее содержимое источника - в зависимости от его типа
  //      content.Parent = this;
  //    }
  //  }

  //  SourceContent content;
  //  public SourceContent Content
  //  {
  //    get { return content; }
  //    set
  //    {
  //      content = value;
  //      currContent[srcType] = value; // содержимое источника для типа - для переключения к уже созданому
  //      currContent[srcType].Parent = this;
  //    }
  //  }

  //  DataTable dt; 
  //  [XmlIgnore]
  //  public DataTable DT { get { return dt; } set { dt = value; } } // данные источника
  //  [XmlIgnore]
  //  public bool InProc { get; set; } // признак работающего процесса получения данных

  //  Dictionary<SourceType, SourceContent> currContent = new Dictionary<SourceType, SourceContent>(); // созданное содержимое по типу
  //  [XmlIgnore]
  //  public DbContent DbSource { get { return (DbContent)GetContent(SourceType.Database); } }
  //  [XmlIgnore]
  //  public ExcelContent ExcelSource { get { return (ExcelContent)GetContent(SourceType.Excel); } }
  //  [XmlIgnore]
  //  public CsvContent CsvSource { get { return (CsvContent)GetContent(SourceType.CSV); } }
  //  //-------------------------------------------------------------------------
  //  public Source()
  //  {
  //    SrcType = SourceType.Database;
  //  }
  //  //-------------------------------------------------------------------------
  //  private SourceContent GetContent(SourceType type) // новое или уже имеющееся содержимое указанного типа
  //  {
  //    if (!currContent.ContainsKey(type))
  //      switch (type)
  //      {
  //        case SourceType.Database:
  //          currContent.Add(type, new DbContent());
  //          break;
  //        case SourceType.Excel:
  //          currContent.Add(type, new ExcelContent());
  //          break;
  //        case SourceType.CSV:
  //          currContent.Add(type, new CsvContent());
  //          break;
  //      }
  //    if (currContent[type] != null) currContent[type].Parent = this;
  //    return currContent[type];
  //  }
  //  //-------------------------------------------------------------------------
  //  public void Check() // проверить содержимое
  //  {
  //    Content.Check();
  //  }
  //  //-------------------------------------------------------------------------
  //  public void DTClear() // очистить данные
  //  {
  //    if (DT != null)
  //    {
  //      //DT.Dispose();
  //      DT = null;
  //      GC.Collect();
  //    }
  //  }
  //}
  ////===========================================================================
  //[XmlInclude(typeof(DbContent))]
  //[XmlInclude(typeof(ExcelContent))]
  //[XmlInclude(typeof(CsvContent))]
  //public abstract class SourceContent // содержимое источника
  //{
  //  protected TaskContext context; // параметры получения данных, если получение - как процесс (например для DbContent)
  //  protected Action<bool> afterGetData; // действие после получения данных
  //  [XmlIgnore]
  //  public virtual Source Parent { get; set; } // источник
  //  [XmlArrayItem("field")]
  //  public List<string> Fields { get; set; } // список наименований полей
  //  [XmlIgnore]
  //  public virtual Exception Error { get { return context != null ? context.Error : null; } } // ошибка получения данных
  //  //-------------------------------------------------------------------------
  //  public virtual void Check() {} // проверка содержимого
  //  //-------------------------------------------------------------------------
  //  public virtual List<string> GetCheckFields()  // проверка и выдача наименований полей
  //  { 
  //    return Fields; 
  //  }
  //  //-------------------------------------------------------------------------
  //  public virtual void GetData(TaskContext c, Action<bool> afterGetDataAction) // запуск получения данных
  //  {
  //    afterGetData = afterGetDataAction; // что вызвать по окончании (штатно - конце GetDataEnd())
  //    context = c ?? new TaskContext();
  //    context.Error = null;
  //    context.Cancel = false;
  //    if (context.OnFinish == null)
  //      context.OnFinish = GetDataEnd; // куда вернуться в конце, если мы запустим как процесс
  //    Parent.InProc = true;
  //    Parent.DTClear();
  //    Parent.DT = null;
  //  }
  //  //-------------------------------------------------------------------------
  //  public virtual void GetDataEnd(object result, string msg) // окончание получения данных
  //  {
  //    bool fail = (context.Cancel || context.Error != null);

  //    if (!fail && result is DataTable)
  //    {
  //      Parent.DT = ((DataTable)result).Copy();
  //      Parent.DT.TableName = Parent.Name;
  //    }

  //    if (context.OnProgress != null)
  //      context.OnProgress(fail ? 0 : int.MaxValue, msg);
      
  //    Parent.InProc = false;
  //    if (afterGetData != null)
  //      afterGetData(fail);
  //  }
  //  //-------------------------------------------------------------------------
  //  public virtual void GetDataStop() { } // прервать получение данных (например если запущено как процесс)
  //  //-------------------------------------------------------------------------
  //  public virtual void GetDataError(string msg, Exception ex) // принять ошибку получения данных из процесса
  //  {
  //    context.Error = ex;
  //  }
  //}
  ////===========================================================================
  //public class DbContent : SourceContent, ISqlModel // содержимое источника БД  
  //{
  //  EncrDecr ed;
  //  SqlController dbController;
  //  ProviderType provider;
  //  //-- ISqlModel Members ---------------------------------------------------
  //  public ProviderType Provider
  //  {
  //    get { return provider; }
  //    set { provider = value; }
  //  }
  //  public string Server { get; set; }
  //  public string DB { get; set; }
  //  public string Login { get; set; }
  //  [XmlIgnore]
  //  public string Pwd { get; set; }
  //  [XmlIgnore]
  //  public string ConnStr { get; set; }
  //  public int CommandTimeout { get; set; }
  //  public string SQL { get; set; }
  //  /* ISqlModel.Fields реализовано в SourceContent */
  //  //-------------------------------------------------------------------------
  //  public string PwdEncr { get { return ed.Encrypt(Pwd); } set { Pwd = ed.Decrypt(value); } } // для сохранения
  //  public string ConnStrEncr { get { return ed.Encrypt(ConnStr); } set { ConnStr = ed.Decrypt(value); } } // для сохранения
  //  [XmlIgnore]
  //  public string ConnectionInfo // краткая инфа для вью
  //  {
  //    get
  //    {
  //      string info, nl = Environment.NewLine;
  //      if (Provider == ProviderType.OleDB || Provider == ProviderType.ODBC)
  //        info = string.Format(@"Source: {0}Connection string: {1}", Provider + nl, ConnStr + nl);
  //      else
  //        info = string.Format(@"Source: {0}Server: {1}DB: {2}Login: {3}",
  //          Provider + nl, Server + nl, DB + nl, Login + nl);
  //      return info;
  //    }
  //  }
  //  //-------------------------------------------------------------------------
  //  public DbContent()
  //  {
  //    Provider = ProviderType.OleDB;
  //    CommandTimeout = 15;
  //    Fields = new List<string>();
  //    ed = new EncrDecr(string.Empty, string.Empty);
  //    dbController = SqlController.CreateSqlController(this);
  //  }
  //  //-------------------------------------------------------------------------
  //  public ISqlView GetSqlView()
  //  {
  //    return dbController.View;
  //  }
  //  //-------------------------------------------------------------------------
  //  public override void Check() 
  //  {
  //    switch (Provider)
  //    {
  //      case ProviderType.MSSQL:
  //      case ProviderType.Oracle:
  //      case ProviderType.Sybase:
  //      case ProviderType.SybaseASE15:
  //        if (string.IsNullOrEmpty(Server))
  //          throw new Exception(Parent.Name + ": server is not specified");
  //        break;
  //      case ProviderType.ODBC:
  //      case ProviderType.OleDB:
  //        if (string.IsNullOrEmpty(ConnStr))
  //          throw new Exception(Parent.Name + ": connection string is not specified");
  //        break;
  //      default:
  //        break;
  //    }
  //    if (string.IsNullOrEmpty(SQL))
  //      throw new Exception(Parent.Name + ": query is not specified");
  //  }
  //  //-------------------------------------------------------------------------
  //  public override List<string> GetCheckFields() 
  //  {
  //    if (Fields.Count == 0) // поля источника БД появляются только после запуска запроса из ISqlView или из сверки
  //      throw new Exception(Parent.Name + ": no fields found. Run query to get!");
  //    return Fields; 
  //  }
  //  //-------------------------------------------------------------------------
  //  public override void GetData(TaskContext c, Action<bool> a)
  //  {
  //    base.GetData(c, a);
  //    dbController.GetData(SQL, context); // запуск процесса начитки данных в SqlController-е, обработчики хода, окончания и ошибки заданы в context
  //    // заполнение SourceContent.DT происходит в базовом GetDataEnd(), который вызывается при успешном окончании запущенного процесса
  //  }
  //  //-------------------------------------------------------------------------
  //  public override void GetDataStop()
  //  {
  //    dbController.StopGetData();
  //  }
  //}
  ////===========================================================================
  //public class ExcelContent : SourceContent  // содержимое источника Excel
  //{
  //  public string Filename { get; set; }
  //  public string Sheet { get; set; }
  //  public string RngStart { get; set; }
  //  public string RngEnd { get; set; }
  //  public bool FirstLineNames { get; set; }
  //  List<string> sheets = new List<string>();
  //  [XmlArrayItem("sheet")]
  //  public List<string> Sheets
  //  {
  //    get { return sheets; }
  //    set { sheets = value; }
  //  }
  //  //-------------------------------------------------------------------------
  //  public ExcelContent()
  //  {
  //    Fields = new List<string>();
  //  }
  //  //-------------------------------------------------------------------------
  //  public override void Check()
  //  {
  //    if (string.IsNullOrEmpty(Filename))
  //      throw new Exception(Parent.Name + ": no file name");
  //  }
  //  //-------------------------------------------------------------------------
  //  public override List<string> GetCheckFields()
  //  {
  //    Check();
  //    GetExcelData(true); 
  //    return Fields;
  //  }
  //  //-------------------------------------------------------------------------
  //  public override void GetData(TaskContext c, Action<bool> a)
  //  {
  //    base.GetData(c, a);
  //    Check();
  //    if (c != null && c.OnProgress != null)
  //      c.OnProgress(0, "Open file \"" + Path.GetFileName(Filename) + "\" ...");
  //    GetExcelData();
  //    GetDataEnd(null, string.Format("{0} rows", Parent.DT != null ? Parent.DT.Rows.Count : 0)); 
  //  }
  //  //-------------------------------------------------------------------------
  //  /* данные из excel - в таблицу или только список имен полей */
  //  void GetExcelData(bool fieldsOnly = false)
  //  {
  //    Excel.Workbook wb = null;
  //    Excel.Worksheet ws = null;
  //    Excel.Range rg = null;
  //    try
  //    {
  //      wb = ExcelProc.GetWorkbook(Path.GetFileName(Filename), Path.GetDirectoryName(Filename), true, false);
  //      ws = ExcelProc.GetSheet(wb, Sheet);
  //      if (ws == null)
  //        throw new Exception(Parent.Name + ": excel sheet \"" + Sheet + "\" not exist");
  //      rg = GetRange(ws);
  //      SheetToTable(ws, rg, fieldsOnly);
  //      if (Parent.DT != null) Parent.DT.TableName = Parent.Name;
  //    }
  //    finally
  //    {
  //      if (wb != null && !wb.Application.Visible)
  //        wb.Application.Quit();
  //      ExcelProc.ReleaseCom(ref wb);
  //      ExcelProc.ReleaseCom(ref ws);
  //      ExcelProc.ReleaseCom(ref rg);
  //    }
  //  }
  //  //-------------------------------------------------------------------------
  //  /* диапазон по заданным границам или вся занятая область */
  //  Excel.Range GetRange(Excel.Worksheet ws)
  //  {
  //    Excel.Range rg;
  //    string rg1 = (RngStart == null ? "" : RngStart.Trim().ToUpper()), rg2 = (RngEnd == null ? "" : RngEnd.Trim().ToUpper());
  //    string patt = @"^[a-zA-Z]{1,2}$|^[1-9]{1}[0-9]{0,4}$|^[a-zA-Z]{1,2}[1-9]{1}[0-9]{0,4}$|^$",
  //      pattR = @"^[1-9]{1}[0-9]{0,4}$", pattC = @"^[a-zA-Z]{1,2}$";

  //    if (!Regex.IsMatch(rg1, patt) || !Regex.IsMatch(rg2, patt))
  //      throw new Exception(string.Format("Range \"{0}:{1}\" is not valid", rg1, rg2));
  //    try
  //    {
  //      int r1 = ws.UsedRange.Row,
  //        c1 = ws.UsedRange.Column,
  //        r2 = ws.UsedRange.Row + ws.UsedRange.Rows.Count - 1,
  //        c2 = ws.UsedRange.Column + ws.UsedRange.Columns.Count - 1;
  //      if (rg1 != "")
  //      {
  //        if (Regex.IsMatch(rg1, pattR)) // это строка
  //          r1 = ((Excel.Range)ws.Rows[rg1]).Row;
  //        else if (Regex.IsMatch(rg1, pattC)) // это столбец
  //          c1 = ((Excel.Range)ws.Columns[rg1]).Column;
  //        else
  //        { r1 = ws.get_Range(rg1).Row; c1 = ws.get_Range(rg1).Column; }
  //      }
  //      if (rg2 != "")
  //      {
  //        if (Regex.IsMatch(rg2, pattR))
  //          r2 = ((Excel.Range)ws.Rows[rg2]).Row;
  //        else if (Regex.IsMatch(rg2, pattC))
  //          c2 = ((Excel.Range)ws.Columns[rg2]).Column;
  //        else
  //        { r2 = ws.get_Range(rg2).Row; c2 = ws.get_Range(rg2).Column; }
  //      }
  //      rg = ws.Range[ws.Cells[r1, c1], ws.Cells[r2, c2]];
  //    }
  //    catch (Exception ex)
  //    {
  //      throw new Exception(string.Format("{0}: can't define range \"{1}:{2}\"", Parent.Name, rg1, rg2), ex);
  //    }
  //    return rg;
  //  }
  //  //-------------------------------------------------------------------------
  //  /* данные в Parent.DT или только имена полей в Fields из указанного диапазона листа excel */
  //  void SheetToTable(Excel.Worksheet ws, Excel.Range rg = null, bool fieldsOnly = false)
  //  {
  //    Fields = new List<string>();
  //    if (!fieldsOnly) Parent.DT = new DataTable();

  //    if (rg == null)
  //      rg = ws.UsedRange;
  //    if (rg.Columns.Count == 0 || rg.Rows.Count == 0)
  //      return;

  //    DataTable dt = new DataTable();

  //    int r0 = (FirstLineNames ? 1 : 0);
  //    //поля        
  //    for (int c = 1; c <= rg.Columns.Count; c++)
  //    {
  //      Type type = typeof(String);
  //      if (FirstLineNames && rg.Cells[1, c] != null 
  //        && rg.Cells[1, c].Value != null && !string.IsNullOrEmpty(rg.Cells[1, c].Value.ToString().Trim()))
  //      {
  //        string colName = rg.Cells[1, c].Value.ToString().Trim();
  //        string tmpName = colName;
  //        int suff = 0;
  //        while (dt.Columns.OfType<DataColumn>().Count(x => x.ColumnName == colName) > 0)
  //          colName = tmpName + "_" + (++suff).ToString();
  //        dt.Columns.Add(colName, type);
  //      }
  //      else
  //        dt.Columns.Add(GetRCName((rg.Cells[1, c] as Excel.Range).get_Address(), false), type);
  //    }
  //    Fields.AddRange(dt.Columns.OfType<DataColumn>().Select(x => x.ColumnName));
      
  //    //данные
  //    if (!fieldsOnly)
  //    {
  //      object[,] dtArr; // для данных Range
  //      if (rg.Columns.Count == 1 && rg.Rows.Count == 1) // т.к.в случае одной ячейки value не возвращает массив
  //      {
  //        dtArr = (object[,])Array.CreateInstance(typeof(Object), new[] { 1, 1 }, new[] { 1, 1 });
  //        dtArr[1, 1] = rg.get_Value();
  //      }
  //      else
  //        dtArr = (object[,])rg.get_Value();

  //      Parent.DT = dt.Clone();
  //      object[] dtRow = new object[dtArr.GetLength(1)]; // для формирования строки
  //      for (int r = r0; r < dtArr.GetLength(0); r++)
  //      {
  //        for (int c = 0; c < dtArr.GetLength(1); c++)
  //          dtRow[c] = dtArr[r + 1, c + 1];
  //        Parent.DT.LoadDataRow(dtRow, true);
  //      }
  //    }
  //  }
  //  //-------------------------------------------------------------------------
  //  string GetRCName(string addr, bool isRow)
  //  {
  //    return addr.ToCharArray().Where(c => (!isRow && char.IsLetter(c)) || (isRow && char.IsDigit(c))).Aggregate("", (current, c) => current + c.ToString());
  //  }
  //}
  ////===========================================================================
  //public class CsvContent : SourceContent  // содержимое источника CSV
  //{
  //  public string Filename { get; set; }
  //  public bool FirstLineNames { get; set; } // получить имена полей из первой строки
  //  public string Delimiter { get; set; }
  //  public string Codepage { get; set; }
  //  //-------------------------------------------------------------------------
  //  public CsvContent()
  //  {
  //    Fields = new List<string>();
  //  }
  //  //-------------------------------------------------------------------------
  //  public override void Check()
  //  {
  //    if (string.IsNullOrEmpty(Filename) || !File.Exists(Filename))
  //      throw new Exception(Parent.Name + ": file not exist");
  //  }
  //  //-------------------------------------------------------------------------
  //  public override List<string> GetCheckFields()
  //  {
  //    Check();
  //    LoadCsvData(true);
  //    return Fields;
  //  }
  //  //-------------------------------------------------------------------------
  //  public override void GetData(TaskContext c, Action<bool> a)
  //  {
  //    base.GetData(c, a);
  //    Check();
  //    LoadCsvData(); // вот тут получаем данные
  //    GetDataEnd(null, string.Format("{0} rows", Parent.DT.Rows.Count)); // чисто для завершения
  //  }
  //  //-------------------------------------------------------------------------
  //  public void LoadCsv(string filename) // вызовем при выборе файла csv
  //  {
  //    if (string.IsNullOrEmpty(filename)) return;
  //    Filename = filename;
  //    LoadCsvData(true); // и сразу запоним список имен полей
  //  }
  //  //-------------------------------------------------------------------------
  //  void LoadCsvData(bool fieldsOnly = false) // получить данные из csv (или только имена полей)
  //  {
  //    Fields = new List<string>();
  //    if (string.IsNullOrEmpty(Filename) || !File.Exists(Filename)) return;

  //    Encoding enc = Encoding.Default;
  //    if (!string.IsNullOrEmpty(Codepage))
  //    {
  //      string cp = Codepage.ToUpper().Trim();
  //      int cpNo = Encoding.Default.CodePage;
  //      switch (cp)
  //      {
  //        case "UTF8":
  //          enc = Encoding.UTF8;
  //          break;
  //        case "UTF7":
  //          enc = Encoding.UTF7;
  //          break;
  //        case "UTF16":
  //          enc = Encoding.Unicode;
  //          break;
  //        case "UTF32":
  //          enc = Encoding.UTF32;
  //          break;
  //        default:
  //          if (string.IsNullOrEmpty(cp))
  //            enc = Encoding.Default;
  //          else if (int.TryParse(cp, out cpNo))
  //            enc = Encoding.GetEncoding(cpNo);
  //          else
  //            enc = Encoding.GetEncoding(cp);
  //          break;
  //      }
  //    }

  //    using (StreamReader rdr = new StreamReader(Filename, enc, true))
  //    {
  //      string[] dlmts = new string[] { string.IsNullOrEmpty(Delimiter) ? Convert.ToString((char)9) : Delimiter };
  //      string flds = rdr.ReadLine();
  //      if (!string.IsNullOrEmpty(flds))
  //      {
  //        Fields = flds.Split(dlmts, StringSplitOptions.None).ToList();
  //        for (int i = 0; i < Fields.Count; i++)
  //        {
  //          if (!FirstLineNames || string.IsNullOrEmpty(Fields[i]))
  //            Fields[i] = string.Format("Field{0}", i+1);
  //        }
  //      }
  //      if (!fieldsOnly)
  //      {
  //        Parent.DT = new DataTable();
  //        for (int i = 0; i < Fields.Count; i++)
  //        {
  //          string tmpName = Fields[i];
  //          int suff = 0;
  //          while (Parent.DT.Columns.OfType<DataColumn>().Count(x => x.ColumnName == Fields[i]) > 0)
  //            Fields[i] = tmpName + "_" + (++suff).ToString();
  //          Parent.DT.Columns.Add(Fields[i]);
  //        }
  //        if (!FirstLineNames)
  //        {
  //          rdr.BaseStream.Seek(0, 0);
  //          rdr.DiscardBufferedData();
  //        }
  //        while (rdr.Peek() >= 0)
  //        {
  //          DataRow dr = Parent.DT.NewRow();
  //          string row = rdr.ReadLine();
  //          if (string.IsNullOrEmpty(row)) continue;
  //          List<string> rowFields = row.Split(dlmts, StringSplitOptions.None).ToList();
  //          for (int i = 0; i < Math.Min(Fields.Count, rowFields.Count); i++)
  //            dr[i] = rowFields[i];
  //          Parent.DT.Rows.Add(dr);
  //        }
  //        Parent.DT.TableName = Parent.Name;
  //      }
  //      rdr.Close();
  //    }
  //  }
  //}
}

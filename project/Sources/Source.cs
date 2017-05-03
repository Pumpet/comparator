using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DataTable = System.Data.DataTable;

namespace Sources
{
  public enum SourceType { Database, Excel, CSV, XML } // виды источников данных
  //===========================================================================
  public class Source // источник данных для сверки
  {
    string name;
    public string Name { get { return string.IsNullOrEmpty(name) ? InnerName : name; } set { name = value; } }
    [XmlIgnore]
    public string InnerName { get; set; } // "внутреннее" имя источника - по умолчанию, для сообщений

    SourceType srcType;
    public SourceType SrcType
    {
      get { return srcType; }
      set
      {
        srcType = value;
        content = GetContent(value); // текущее содержимое источника - в зависимости от его типа
        content.Parent = this;
      }
    }

    SourceContent content;
    public SourceContent Content
    {
      get { return content; }
      set
      {
        content = value;
        currContent[srcType] = value; // содержимое источника для типа - для переключения к уже созданому
        currContent[srcType].Parent = this;
      }
    }

    DataTable dt;
    [XmlIgnore]
    public DataTable DT { get { return dt; } set { dt = value; } } // данные источника
    [XmlIgnore]
    public bool InProc { get; set; } // признак работающего процесса получения данных

    Dictionary<SourceType, SourceContent> currContent = new Dictionary<SourceType, SourceContent>(); // созданное содержимое по типу
    [XmlIgnore]
    public DbContent DbSource { get { return (DbContent)GetContent(SourceType.Database); } }
    [XmlIgnore]
    public ExcelContent ExcelSource { get { return (ExcelContent)GetContent(SourceType.Excel); } }
    [XmlIgnore]
    public CsvContent CsvSource { get { return (CsvContent)GetContent(SourceType.CSV); } }
    [XmlIgnore]
    public XmlContent XmlSource { get { return (XmlContent)GetContent(SourceType.XML); } }

    //-------------------------------------------------------------------------
    public Source()
    {
      SrcType = SourceType.Database;
    }
    //-------------------------------------------------------------------------
    private SourceContent GetContent(SourceType type) // новое или уже имеющееся содержимое указанного типа
    {
      if (!currContent.ContainsKey(type))
        switch (type)
        {
          case SourceType.Database:
            currContent.Add(type, new DbContent());
            break;
          case SourceType.Excel:
            currContent.Add(type, new ExcelContent());
            break;
          case SourceType.CSV:
            currContent.Add(type, new CsvContent());
            break;
          case SourceType.XML:
            currContent.Add(type, new XmlContent());
            break;
        }
      if (currContent[type] != null) currContent[type].Parent = this;
      return currContent[type];
    }
    //-------------------------------------------------------------------------
    public void Check() // проверить содержимое
    {
      Content.Check();
    }
    //-------------------------------------------------------------------------
    public void DTClear() // очистить данные
    {
      if (DT != null)
      {
        //DT.Dispose();
        DT = null;
        GC.Collect();
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Common;

namespace Sources
{
  public enum XlstProcType { None, Script, File }
  //===========================================================================
  public class XmlContent : SourceContent // содержимое источника XML
  {
    public string NameXml { get; set; } // путь к xml-файлу
    public string NameXslt { get; set; } // путь к xslt-файлу (актуально для XsltFromFile)
    public string Codepage { get; set; } // кодировка xml-файла
    public string XsltScript { get; set; } // строка xslt (актуально для XsltFromScript)

    XlstProcType xsltType = XlstProcType.None;
    public XlstProcType XsltType
    {
      get { return xsltType; }
      set { xsltType = value; }
    }
    [XmlIgnore]
    public bool XsltNone // не будет xslt-преобразования
    {
      get { return XsltType == XlstProcType.None; }
      set { if (value) XsltType = XlstProcType.None; }
    }
    [XmlIgnore]
    public bool XsltFromScript // xslt возьмем из XsltScript
    {
      get { return XsltType == XlstProcType.Script; }
      set { if (value) XsltType = XlstProcType.Script; }
    }
    [XmlIgnore]
    public bool XsltFromFile // xslt возьмем из файла NameXslt
    {
      get { return XsltType == XlstProcType.File; }
      set { if (value) XsltType = XlstProcType.File; }
    }

    public string PathRow { get; set; } // XPath к тэгу, определяющему строку
    public bool DataFromTag { get; set; } // определять поля по дочерним тэгам тэга, определяющего строку
    public bool DataFromAttr { get; set; } // определять поля по атрибутам тэга, определяющего строку
    public bool UseFieldsMap { get; set; } // определять поля по путям из fieldMaps

    List<FieldMap> fieldsMap = new List<FieldMap>(); // список соответствий (имя поля:путь относительно тэга, определяющего строку:значение по умолчанию) - актуально для UseFieldsMap
    public List<FieldMap> FieldsMap
    {
      get { return fieldsMap; }
      set { fieldsMap = value; }
    }

    [XmlIgnore]
    public string Info // для отображения краткой информации на SourcePanel
    {
      get
      {
        string nl = Environment.NewLine;
        string info = string.Format(@"XML: {0}XSLT: {1}ROW: {2}FIELDS: {3}",
          NameXml + nl,
          (XsltType == XlstProcType.None ? "not used" : XsltType == XlstProcType.Script ? "script" : NameXslt) + nl,
          PathRow + nl,
          UseFieldsMap ? "map options" : DataFromTag && DataFromAttr ? "row tags/attributes" : DataFromTag ? "row tags" : "row attributes");
        return info;
      }
    }

    //-------------------------------------------------------------------------
    public XmlContent()
    {
      Fields = new List<string>();
      DataFromTag = true;
      DataFromAttr = true;
    }
    //-------------------------------------------------------------------------
    public override void Check()
    {
      if (string.IsNullOrEmpty(NameXml) || !File.Exists(CommonProc.GetFilePath(NameXml)))
        throw new Exception(Parent.Name + ": XML file not exist");
      if (XsltType == XlstProcType.File && (string.IsNullOrEmpty(NameXslt) || !File.Exists(CommonProc.GetFilePath(NameXslt))))
        throw new Exception(Parent.Name + ": XSLT file not exist");
    }
    //-------------------------------------------------------------------------
    public override List<string> GetCheckFields()
    {
      Check();
      LoadXmlData(true); // тут получаем поля в Fields 
      return Fields;
    }
    //-------------------------------------------------------------------------
    public override void GetData(TaskContext c, Action<bool> a)
    {
      base.GetData(c, a);
      Check();
      if (c != null && c.OnProgress != null)
        c.OnProgress(0, "open and transform \"" + Path.GetFileName(NameXml) + "\" ...");
      LoadXmlData(); // тут получаем данные в Parent.DT
      GetDataEnd(null, string.Format("{0} rows", Parent.DT.Rows.Count)); // чисто для завершения
    }
    //-------------------------------------------------------------------------
    void LoadXmlData(bool fieldsOnly = false) // получить данные или только имена полей (fieldsOnly)
    {
      Fields = new List<string>();
      DataTable dt;
      try
      {
        XmlDocument xml = XmlController.GetXml(CommonProc.GetFilePath(NameXml), Codepage);
        if (!XsltNone)
          xml = XmlController.Transform(xml, XsltFromFile ? CommonProc.GetFilePath(NameXslt) : XsltScript, XsltFromFile);

        if (UseFieldsMap)
          dt = XmlController.GetData(xml, PathRow, FieldsMap, fieldsOnly);
        else
          dt = XmlController.GetData(xml, PathRow, DataFromTag, DataFromAttr, fieldsOnly);

        Fields = dt.Columns.OfType<DataColumn>().Select(x => x.ColumnName).ToList();

        if (!fieldsOnly)
        {
          Parent.DT = dt.Copy();
          Parent.DT.TableName = Parent.Name;
        }
      }
      catch (Exception ex)
      {
        throw new Exception(Parent.Name + ": " + ex.Message, ex);
      }
    }
  }
  //===========================================================================
  public class FieldMap // соответствие поля и XPath
  {
    public string FieldName { get; set; } // имя поля, в которое попадут данные xml ...
    public string Path { get; set; } // ... взятые по этому пути (XPath относительно тэга, определяющего строку)
    public string Default { get; set; } // значение по умолчанию, если путь не найден или данных нет (NULL)
  }
}

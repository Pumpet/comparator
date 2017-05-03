using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Sources
{
  public static class XmlController
  {
    public static DataTable Dt { get; set; } // таблица, сформированная по данным Xml
    public static List<FieldMap> Fields { get; set; } // поля таблицы, сформированной по данным Xml (имя поля:путь относительно тэга, определяющего строку:значение по умолчанию)
    static FormXml form;
    //-------------------------------------------------------------------------
    /* форма для XmlContent */
    public static FormXml GetForm(object data)
    {
      if (form == null)
        form = new FormXml();
      form.SetData(data);
      return form;
    }
    //-------------------------------------------------------------------------
    public static void FreeForm()
    {
      form = null;
      GC.Collect();
    }
    //-------------------------------------------------------------------------
    /* Xml из файла в заданной кодировке */
    public static XmlDocument GetXml(string xmlPath, string codepage)
    {
      if (string.IsNullOrEmpty(xmlPath))
        return null;
      XmlDocument xml = new XmlDocument() { PreserveWhitespace = true };
      codepage = codepage ?? "";
      try
      {
        if (!File.Exists(xmlPath))
          throw new Exception("XML file not exists");
        if (!string.IsNullOrEmpty(codepage.Trim()))
        {
          Encoding enc;
          string cp = codepage.ToUpper();
          switch (cp)
          {
            case "UTF8":
              enc = Encoding.UTF8;
              break;
            case "UTF7":
              enc = Encoding.UTF7;
              break;
            case "UTF16":
              enc = Encoding.Unicode;
              break;
            case "UTF32":
              enc = Encoding.UTF32;
              break;
            default:
              int cpNo;
              enc = int.TryParse(cp, out cpNo) ? Encoding.GetEncoding(cpNo) : Encoding.GetEncoding(cp);
              break;
          }
          using (StreamReader sr = new StreamReader(xmlPath, enc))
          {
            xml.Load(sr);
          }
        }
        else
          xml.Load(xmlPath);
      }
      catch (Exception ex)
      {
        throw new Exception("Getting XML Error: " + ex.Message, ex);
      }
      return xml;
    }
    //-------------------------------------------------------------------------
    /* преобразование Xml с помощью xslt из файла или строки */
    public static XmlDocument Transform(XmlDocument xml, string xsl, bool xslIsFile)
    {
      if (string.IsNullOrEmpty(xsl) || xml == null)
        return xml;
      try
      {
        XslCompiledTransform trans = new XslCompiledTransform();
        XsltSettings xslSett = new XsltSettings(true,true);
        if (xslIsFile)
        {
          if (!File.Exists(xsl))
            throw new Exception("XSLT file not exists");
          trans.Load(xsl, xslSett, null);
        }
        else
        {
          XmlDocument xslDoc = new XmlDocument();
          xslDoc.LoadXml(xsl);
          trans.Load(xslDoc, xslSett, null);
        }
        StringBuilder str = new StringBuilder();
        XmlWriter writer = XmlWriter.Create(str, trans.OutputSettings);
        trans.Transform(xml, null, writer);
        xml.LoadXml(str.ToString());
      }
      catch (Exception ex)
      {
        throw new Exception("Transformation Error: " + ex.Message, ex);
      }
      return xml;
    }
    //-------------------------------------------------------------------------
    /* Xml - в таблицу (xpath задает путь к тэгу, определяющему строку)
       определять поля по дочерним тэгам (useTags) и/или атрибутам (useAttrs) тэга, определяющего строку
       schemaOnly - вернуть пустую таблицу */
    public static DataTable GetData(XmlDocument xml, string xpath, bool useTags, bool useAttrs, bool schemaOnly = false)
    {
      DataTable dt = new DataTable();
      Fields = new List<FieldMap>();
      if (xml == null) return dt;
      if (!useTags & !useAttrs) useTags = true;
      try
      {
        List<XElement> xrows = GetRows(xml, xpath);
        List<string> fields = xrows.Elements().Where(x => useTags).Select(x => x.Name.ToString())
            .Union(xrows.Where(x => useAttrs).Attributes().Select(a => a.Name.ToString())).Distinct().ToList();
        if (fields.Count == 0)
          throw new Exception("Fields not found!");

        foreach (var field in fields)
        {
          dt.Columns.Add(field);
          string path = null;
          if (useTags && xrows.Elements(field).Any())
            path = field;
          else if (useAttrs && xrows.Attributes(field).Any())
            path = "@" + field;
          Fields.Add(new FieldMap() { FieldName = field, Path = path });
        }

        if (schemaOnly)
          return dt;

        foreach (var xrow in xrows)
        {
          DataRow row = dt.Rows.Add();
          foreach (var field in fields)
          {
            row[field] = DBNull.Value;
            if (useTags)
            {
              XElement xele = xrow.Element(field);
              if (xele != null) 
                row[field] = xele.Nodes().FirstOrDefault(x => x.NodeType == XmlNodeType.Text);
            }
            if (useAttrs && row[field] == DBNull.Value)
            {
              XAttribute xattr = xrow.Attribute(field);
              if (xattr != null) row[field] = xattr.Value;
            }
          }
          if (!row.ItemArray.Any(x => x != DBNull.Value && x != null))
            dt.Rows.Remove(row);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Convert error: " + ex.Message, ex);
      }
      return dt;
    }
    //-------------------------------------------------------------------------
    /* Xml - в таблицу (xpath задает путь к тэгу, определяющему строку)
       определять поля по fieldMaps (имя поля:путь относительно тэга, определяющего строку:значение по умолчанию)
       schemaOnly - вернуть пустую таблицу */
    public static DataTable GetData(XmlDocument xml, string xpath, List<FieldMap> fieldMaps, bool schemaOnly = false)
    {
      DataTable dt = new DataTable();
      Fields = new List<FieldMap>();
      if (xml == null) return dt;
      try
      {
        if (fieldMaps == null || fieldMaps.Count == 0)
          throw new Exception("Fields map not defined!");
        if (fieldMaps.Any(x => fieldMaps.Exists(y => y.FieldName == x.FieldName && y != x)))
          throw new Exception("Fields map has duplicated fields!");

        foreach (var fm in fieldMaps)
        {
          dt.Columns.Add(fm.FieldName);
          Fields.Add(new FieldMap() { FieldName = fm.FieldName, Path = fm.Path, Default = fm.Default });
        }

        if (schemaOnly)
          return dt;

        List<XElement> xrows = GetRows(xml, xpath);
        foreach (var xrow in xrows)
        {
          DataRow row = dt.Rows.Add();
          foreach (var fm in fieldMaps)
          {
            row[fm.FieldName] = fm.Default == null ? DBNull.Value : (object)fm.Default;
            object xnode = ((IEnumerable<object>)xrow.XPathEvaluate(fm.Path)).FirstOrDefault();
            if (xnode is XElement)
              row[fm.FieldName] = ((XElement)xnode).Nodes().FirstOrDefault(x => x.NodeType == XmlNodeType.Text);
            else if (xnode is XAttribute)
              row[fm.FieldName] = ((XAttribute)xnode).Value;
          }
          if (!row.ItemArray.Any(x => x != DBNull.Value && x != null))
            dt.Rows.Remove(row);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Convert from fields map error: " + ex.Message, ex);
      }
      return dt;
    }
    //-------------------------------------------------------------------------
    /* тэги, определяющие строки таблицы - отбор по xpath */
    static List<XElement> GetRows(XmlDocument xml, string xpath)
    {
      List<XElement> xrows = new List<XElement>();
      xpath = xpath.Trim();
      if (string.IsNullOrEmpty(xpath))
        throw new Exception("Path to row's node not exists!");
      XDocument xdoc = XDocument.Parse(xml.OuterXml);
      try
      {
        xrows = xdoc.Root.XPathSelectElements(xpath).ToList();
      }
      catch (Exception ex)
      {
        throw new Exception("XPath Error (" + ex.Message + ")", ex);
      }
      return xrows;
    }
  }

}

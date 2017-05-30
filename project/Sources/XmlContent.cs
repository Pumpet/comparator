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
  public class XmlContent : SourceContent
  {
    public string NameXml { get; set; } // path to XML-file
    public string NameXslt { get; set; } // path to XSL-file
    public string Codepage { get; set; } 
    public string XsltScript { get; set; } // xsl script text

    XlstProcType xsltType = XlstProcType.None;
    public XlstProcType XsltType
    {
      get { return xsltType; }
      set { xsltType = value; }
    }
    [XmlIgnore]
    public bool XsltNone // do not use xslt
    {
      get { return XsltType == XlstProcType.None; }
      set { if (value) XsltType = XlstProcType.None; }
    }
    [XmlIgnore]
    public bool XsltFromScript // xslt is described in XsltScript
    {
      get { return XsltType == XlstProcType.Script; }
      set { if (value) XsltType = XlstProcType.Script; }
    }
    [XmlIgnore]
    public bool XsltFromFile // xslt is described in xsl-file (NameXslt)
    {
      get { return XsltType == XlstProcType.File; }
      set { if (value) XsltType = XlstProcType.File; }
    }

    public string PathRow { get; set; } // XPath for tags which will be used as basis for table data row
    public bool DataFromTag { get; set; } // fields will be defined by child tags of tags selected according PathRow
    public bool DataFromAttr { get; set; } // fields will be defined by attributes of tags selected according PathRow
    public bool UseFieldsMap { get; set; } // fields will be defined by paths from fieldMaps

    // list of (field name : path based on the tag defined in PathRow : default value)
    List<FieldMap> fieldsMap = new List<FieldMap>(); 
    public List<FieldMap> FieldsMap
    {
      get { return fieldsMap; }
      set { fieldsMap = value; }
    }

    [XmlIgnore]
    public string Info // short info for View (SourcePanel)
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
      LoadXmlData(true); 
      return Fields;
    }
    //-------------------------------------------------------------------------
    public override void GetData(TaskContext c, Action<bool> a)
    {
      base.GetData(c, a);
      Check();
      if (c != null && c.OnProgress != null)
        c.OnProgress(0, "open and transform \"" + Path.GetFileName(NameXml) + "\" ...");
      LoadXmlData(); 
      GetDataEnd(null, string.Format("{0} rows", Parent.DT.Rows.Count)); 
    }
    //-------------------------------------------------------------------------
    /* Transform and convert xml to table, get data to Parent.DT, field names to Fields */
    void LoadXmlData(bool fieldsOnly = false) 
    {
      Fields = new List<string>();
      DataTable dt;
      try
      {
        XmlDocument xml = XmlController.GetXml(CommonProc.GetFilePath(NameXml), Codepage);
        if (!XsltNone)
          xml = XmlController.Transform(xml, XsltFromFile ? CommonProc.GetFilePath(NameXslt) : XsltScript, XsltFromFile);

        if (UseFieldsMap)
          dt = XmlController.GetData(xml, PathRow, FieldsMap, fieldsOnly); // getting data based on FieldMap 
        else
          dt = XmlController.GetData(xml, PathRow, DataFromTag, DataFromAttr, fieldsOnly); // getting data from tags/attributes

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
  /* Describes field, path to it and default value */
  public class FieldMap 
  {
    public string FieldName { get; set; } // field name for xml data
    public string Path { get; set; } // XPath relative to tag which will be used as basis for the table data row
    public string Default { get; set; } // default value - if path not found or no data
  }
}

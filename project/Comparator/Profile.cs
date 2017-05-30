using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.IO;
using Sources;

namespace Comparator
{
  public enum ViewResultType { Excel, HTML } 
  public enum MailResult { Attachment, GZIP, Text, Link}
  //===========================================================================
  /* Profile - describes comparison rules, sourses and its field pairs */
  public class Profile : IDisposable
  {
    [XmlIgnore]
    public bool needSave { get; set; } // has unsaved changes
    [XmlIgnore]
    public string Filepath { get; set; } // path to saved(serialized) file
    [XmlIgnore]
    public string ProfileName 
    {
      get
      {
        if (string.IsNullOrEmpty(Filepath))
          return SrcA.Name + "_" + SrcB.Name;
        else
          return Path.GetFileNameWithoutExtension(Filepath);
      }
    }
    [XmlIgnore]
    public string ViewCaption // header for view
    {
      get
      {
        if (string.IsNullOrEmpty(Filepath))
          return "New profile" + (needSave ? "*" : "");
        else
          return Path.GetFileNameWithoutExtension(Filepath) + (needSave ? "*" : "");
      }
    }

    // comparison options and rules:
    bool diffOnly = true; // get only differences in comparison result
    public bool DiffOnly { get { return diffOnly; } set { diffOnly = value; } }
    bool onlyA = true; // get not matched records of source A in comparison result
    public bool OnlyA { get { return onlyA; } set { onlyA = value; } }
    bool onlyB = true; // get not matched records of source B in comparison result
    public bool OnlyB { get { return onlyB; } set { onlyB = value; } }
    public bool MatchInOrder { get; set; } // match records in order, not by keys
    public bool MatchAllPairs { get; set; } // match all fields
    public bool CheckRepeats { get; set; }
    public bool TryConvert { get; set; }
    public bool NullAsStr { get; set; }
    public bool CaseSens { get; set; }

    // options for batch-mode:
    public bool Send { get; set; } // send mail
    public string SendTo { get; set; } // recipients
    public string Subject { get; set; } // subject
    ViewResultType resType = ViewResultType.Excel; // type of result file
    public ViewResultType ResType { get { return resType; } set { resType = value; } }
    public string ResFolder { get; set; } // path for result file
    public string ResFile { get; set; }   // result file name
    public bool TimeInResFile { get; set; }   // include timestamp in result file name 
    MailResult resMail = MailResult.Attachment; // way to send result in mail
    public MailResult ResMail { get { return resMail; } set { resMail = value; } }
    [XmlIgnore]
    public bool ResExcel { get { return ResType == ViewResultType.Excel; } set { if (value) ResType = ViewResultType.Excel; } }
    [XmlIgnore]
    public bool ResHTML { get { return ResType == ViewResultType.HTML; } set { if (value) ResType = ViewResultType.HTML; } }

    Source srcA = new Source() { InnerName = "SourceA" };
    public Source SrcA // object for source A
    {
      get { return srcA; }
      set { srcA = value; srcA.InnerName = "SourceA"; }
    }
    Source srcB = new Source() { InnerName = "SourceB" };
    public Source SrcB // object for source B
    {
      get { return srcB; }
      set { srcB = value; srcB.InnerName = "SourceB"; }
    }

    // field pairs
    List<ColPair> cols = new List<ColPair>();
    public List<ColPair> Cols
    {
      get { return cols; }
      set { cols = value; }
    }
    //-------------------------------------------------------------------------
    /* lists of source fields */
    public List<string>[] GetFields(bool check)
    {
      return new[] { 
        check ? SrcA.Content.GetCheckFields() : SrcA.Content.Fields, 
        check ? SrcB.Content.GetCheckFields() : SrcB.Content.Fields };
    }
    //-------------------------------------------------------------------------
    /* check ready for comparison */
    public void Check() 
    {
      if (Cols.Count == 0)
        throw new Exception("No fields in pairs");
      SrcA.Check();
      SrcB.Check();
    }
    //-------------------------------------------------------------------------
    /* check difference between field names in pairs and in sources */
    public void CheckFieldPairs(bool loadFields)
    {
      List<string>[] flds = GetFields(loadFields);
      if (Cols.Exists(x => { return (!string.IsNullOrEmpty(x.ColA) && !flds[0].Contains(x.ColA)) || (!string.IsNullOrEmpty(x.ColB) && !flds[1].Contains(x.ColB)); }))
        throw new Exception("Some of fields in pairs not exist in sources");
    }
    //-------------------------------------------------------------------------
    /* prepare profile for save or for comparison */
    public void Prepare()
    {
      if (string.IsNullOrEmpty(SrcA.Name)) SrcA.Name = SrcA.InnerName;
      if (string.IsNullOrEmpty(SrcB.Name)) SrcB.Name = SrcB.InnerName;
      Cols.RemoveAll(x => { return (string.IsNullOrEmpty(x.ColA) && string.IsNullOrEmpty(x.ColB)); });
      foreach (var pair in Cols.Where(x => { return (string.IsNullOrEmpty(x.ColA) || string.IsNullOrEmpty(x.ColB)); }))
      {
        pair.Key = false;
        pair.Match = false;
      }
    }
    //-------------------------------------------------------------------------
    public void Dispose()
    {
      if (SrcA != null) SrcA.DTClear();
      if (SrcB != null) SrcB.DTClear();
    }
  }
  //===========================================================================
  /* field names pair */
  public class ColPair
  {
    public bool Key { get; set; } // key fields - use for record matching
    public bool Match { get; set; } // fields for comparision in matched records
    public string ColA { get; set; }
    public string ColB { get; set; }
  }
}

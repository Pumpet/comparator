using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.IO;
using Sources;

namespace Comparator
{
  public enum ViewResultType { Excel, HTML } // варианты выдачи/просмотра результата
  public enum MailResult { Attachment, GZIP, Text, Link}
  //===========================================================================
  public class Profile : IDisposable
  {
    [XmlIgnore]
    public bool needSave { get; set; } // были изменения
    [XmlIgnore]
    public string Filepath { get; set; }
    [XmlIgnore]
    public string ProfileName // имя файла профиля
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
    public string ViewCaption // заголовок для вью
    {
      get
      {
        if (string.IsNullOrEmpty(Filepath))
          return "New profile" + (needSave ? "*" : "");
        else
          return Path.GetFileNameWithoutExtension(Filepath) + (needSave ? "*" : "");
      }
    }
    bool diffOnly = true;
    public bool DiffOnly { get { return diffOnly; } set { diffOnly = value; } }
    bool onlyA = true;
    public bool OnlyA { get { return onlyA; } set { onlyA = value; } }
    bool onlyB = true;
    public bool OnlyB { get { return onlyB; } set { onlyB = value; } }
    public bool MatchInOrder { get; set; }
    public bool MatchAllPairs { get; set; }
    public bool CheckRepeats { get; set; }
    public bool TryConvert { get; set; }
    public bool NullAsStr { get; set; }
    public bool CaseSens { get; set; }
    public bool Send { get; set; } // будем отправлять (пакетный режим)
    public string SendTo { get; set; } // (пакетный режим если Send = true)
    ViewResultType resType = ViewResultType.Excel; // тип результата (пакетный режим)
    public ViewResultType ResType { get { return resType; } set { resType = value; } }
    public string ResFolder { get; set; } // путь для результата (пакетный режим)
    public string ResFile { get; set; }   // имя файла результата (пакетный режим)
    public bool TimeInResFile { get; set; }   // в имя файла результата включать текущие дату-время (пакетный режим)
    MailResult resMail = MailResult.Attachment; // результат в письме (пакетный режим если Send = true)
    public MailResult ResMail { get { return resMail; } set { resMail = value; } }
    public string Subject { get; set; } // тема письма (пакетный режим если Send = true)
    [XmlIgnore]
    public bool ResExcel { get { return ResType == ViewResultType.Excel; } set { if (value) ResType = ViewResultType.Excel; } }
    [XmlIgnore]
    public bool ResHTML { get { return ResType == ViewResultType.HTML; } set { if (value) ResType = ViewResultType.HTML; } }
    Source srcA = new Source() { InnerName = "SourceA" };
    public Source SrcA // источник A
    {
      get { return srcA; }
      set { srcA = value; srcA.InnerName = "SourceA"; }
    }
    Source srcB = new Source() { InnerName = "SourceB" };
    public Source SrcB // источник B
    {
      get { return srcB; }
      set { srcB = value; srcB.InnerName = "SourceB"; }
    }
    List<ColPair> cols = new List<ColPair>();
    public List<ColPair> Cols
    {
      get { return cols; }
      set { cols = value; }
    }
    //-------------------------------------------------------------------------
    /* списки имен полей источников */
    public List<string>[] GetFields(bool check)
    {
      return new[] { 
        check ? SrcA.Content.GetCheckFields() : SrcA.Content.Fields, 
        check ? SrcB.Content.GetCheckFields() : SrcB.Content.Fields };
    }
    //-------------------------------------------------------------------------
    /* проверка готовности к сверке */
    public void Check() 
    {
      if (Cols.Count == 0)
        throw new Exception("No fields in pairs");
      SrcA.Check();
      SrcB.Check();
    }
    //-------------------------------------------------------------------------
    /* проверка соответствия имен полей в парах и в источниках */
    public void CheckFieldPairs(bool loadFields)
    {
      List<string>[] flds = GetFields(loadFields);
      if (Cols.Exists(x => { return (!string.IsNullOrEmpty(x.ColA) && !flds[0].Contains(x.ColA)) || (!string.IsNullOrEmpty(x.ColB) && !flds[1].Contains(x.ColB)); }))
        throw new Exception("Some of fields in pairs not exist in sources");
    }
    //-------------------------------------------------------------------------
    /* подготовка профиля к записи или сверке */
    public void Prepare()
    {
      if (string.IsNullOrEmpty(SrcA.Name)) SrcA.Name = SrcA.InnerName;
      if (string.IsNullOrEmpty(SrcB.Name)) SrcB.Name = SrcB.InnerName;
      // не д.б. пустых пар
      Cols.RemoveAll(x => { return (string.IsNullOrEmpty(x.ColA) && string.IsNullOrEmpty(x.ColB)); });
      // одно из полей пустое - только для выдачи
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
  /* пара полей */
  public class ColPair
  {
    public bool Key { get; set; }
    public bool Match { get; set; }
    public string ColA { get; set; }
    public string ColB { get; set; }
  }
}

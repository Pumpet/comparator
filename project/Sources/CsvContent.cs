using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Common;

namespace Sources
{
  public class CsvContent : SourceContent 
  {
    public string Filename { get; set; }
    public bool FirstLineNames { get; set; } // true if field names must be in the first string
    public string Delimiter { get; set; }
    public string Codepage { get; set; }
    //-------------------------------------------------------------------------
    public CsvContent()
    {
      Fields = new List<string>();
    }
    //-------------------------------------------------------------------------
    public override void Check()
    {
      if (string.IsNullOrEmpty(Filename))
        throw new Exception(Parent.Name + ": file name not defined");
      if (!File.Exists(CommonProc.GetFilePath(Filename)))
        throw new Exception(Parent.Name + ": file not exist");
    }
    //-------------------------------------------------------------------------
    public override List<string> GetCheckFields()
    {
      Check();
      LoadCsvData(true);
      return Fields;
    }
    //-------------------------------------------------------------------------
    public override void GetData(TaskContext c, Action<bool> a)
    {
      base.GetData(c, a);
      Check();
      if (c != null && c.OnProgress != null)
        c.OnProgress(0, "open file \"" + Path.GetFileName(Filename) + "\" ...");
      LoadCsvData(); 
      GetDataEnd(null, string.Format("{0} rows", Parent.DT.Rows.Count)); 
    }
    //-------------------------------------------------------------------------
    /* When select a file */
    public void LoadCsv(string filename) 
    {
      if (string.IsNullOrEmpty(filename)) return;
      Filename = filename;
      LoadCsvData(true); 
    }
    //-------------------------------------------------------------------------
    /* Get data from file to Parent.DT, field names to Fields */
    void LoadCsvData(bool fieldsOnly = false) 
    {
      Fields = new List<string>();
      if (string.IsNullOrEmpty(Filename) || !File.Exists(CommonProc.GetFilePath(Filename))) return;

      Encoding enc = Encoding.Default;
      if (!string.IsNullOrEmpty(Codepage))
      {
        string cp = Codepage.ToUpper().Trim();
        int cpNo = Encoding.Default.CodePage;
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
            if (string.IsNullOrEmpty(cp))
              enc = Encoding.Default;
            else if (int.TryParse(cp, out cpNo))
              enc = Encoding.GetEncoding(cpNo);
            else
              enc = Encoding.GetEncoding(cp);
            break;
        }
      }

      using (StreamReader rdr = new StreamReader(CommonProc.GetFilePath(Filename), enc, true))
      {
        string[] dlmts = new string[] { string.IsNullOrEmpty(Delimiter) ? Convert.ToString((char)9) : Delimiter };
        string flds = rdr.ReadLine();
        if (!string.IsNullOrEmpty(flds))
        {
          Fields = flds.Split(dlmts, StringSplitOptions.None).ToList();
          for (int i = 0; i < Fields.Count; i++)
          {
            if (!FirstLineNames || string.IsNullOrEmpty(Fields[i]))
              Fields[i] = string.Format("Field{0}", i + 1);
          }
        }
        if (!fieldsOnly)
        {
          Parent.DT = new DataTable();
          for (int i = 0; i < Fields.Count; i++)
          {
            string tmpName = Fields[i];
            int suff = 0;
            while (Parent.DT.Columns.OfType<DataColumn>().Count(x => x.ColumnName == Fields[i]) > 0)
              Fields[i] = tmpName + "_" + (++suff).ToString();
            Parent.DT.Columns.Add(Fields[i]);
          }
          if (!FirstLineNames)
          {
            rdr.BaseStream.Seek(0, 0);
            rdr.DiscardBufferedData();
          }
          while (rdr.Peek() >= 0)
          {
            DataRow dr = Parent.DT.NewRow();
            string row = rdr.ReadLine();
            if (string.IsNullOrEmpty(row)) continue;
            List<string> rowFields = row.Split(dlmts, StringSplitOptions.None).ToList();
            for (int i = 0; i < Math.Min(Fields.Count, rowFields.Count); i++)
              dr[i] = rowFields[i];
            Parent.DT.Rows.Add(dr);
          }
          Parent.DT.TableName = Parent.Name;
        }
        rdr.Close();
      }
    }
  }
}

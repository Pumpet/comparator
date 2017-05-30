using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Common;

namespace Sources
{
  /* Base class for "Content"-classes that getting data from specific type sources */
  [XmlInclude(typeof(DbContent))]
  [XmlInclude(typeof(ExcelContent))]
  [XmlInclude(typeof(CsvContent))]
  [XmlInclude(typeof(XmlContent))]
  public abstract class SourceContent 
  {
    protected TaskContext context; // parameters for getting data process
    protected Action<bool> afterGetData; // action after getting data
    [XmlIgnore]
    public virtual Source Parent { get; set; } // object that uses data in comparison
    [XmlArrayItem("field")]
    public List<string> Fields { get; set; } // field names
    [XmlIgnore]
    public virtual Exception Error { get { return context != null ? context.Error : null; } } // last getting data exception
    //-------------------------------------------------------------------------
    /* check content */
    public virtual void Check() { } 
    //-------------------------------------------------------------------------
    /* check and get fields */
    public virtual List<string> GetCheckFields()  
    {
      return Fields;
    }
    //-------------------------------------------------------------------------
    /* start getting data process */
    public virtual void GetData(TaskContext c, Action<bool> afterGetDataAction) 
    {
      afterGetData = afterGetDataAction; 
      context = c ?? new TaskContext();
      context.Error = null;
      context.Cancel = false;
      if (context.OnFinish == null)
        context.OnFinish = GetDataEnd; // end of process action
      Parent.InProc = true;
      Parent.DTClear();
      Parent.DT = null;
    }
    //-------------------------------------------------------------------------
    /* end of getting data process */
    public virtual void GetDataEnd(object result, string msg)
    {
      bool fail = (context.Cancel || context.Error != null);

      if (!fail && result is DataTable)
      {
        Parent.DT = ((DataTable)result).Copy();
        Parent.DT.TableName = Parent.Name;
      }

      if (context.OnProgress != null)
        context.OnProgress(fail ? 0 : int.MaxValue, msg);

      Parent.InProc = false;
      if (afterGetData != null)
        afterGetData(fail);
    }
    //-------------------------------------------------------------------------
    /* stop getting data process */
    public virtual void GetDataStop() { } 
    //-------------------------------------------------------------------------
    /* error of getting data process */
    public virtual void GetDataError(string msg, Exception ex) 
    {
      context.Error = ex;
    }
  }
}

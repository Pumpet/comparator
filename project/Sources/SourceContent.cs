using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Common;

namespace Sources
{
  [XmlInclude(typeof(DbContent))]
  [XmlInclude(typeof(ExcelContent))]
  [XmlInclude(typeof(CsvContent))]
  [XmlInclude(typeof(XmlContent))]
  public abstract class SourceContent // содержимое источника
  {
    protected TaskContext context; // параметры получения данных, если получение - как процесс (например для DbContent)
    protected Action<bool> afterGetData; // действие после получения данных
    [XmlIgnore]
    public virtual Source Parent { get; set; } // источник
    [XmlArrayItem("field")]
    public List<string> Fields { get; set; } // список наименований полей
    [XmlIgnore]
    public virtual Exception Error { get { return context != null ? context.Error : null; } } // ошибка получения данных
    //-------------------------------------------------------------------------
    public virtual void Check() { } // проверка содержимого
    //-------------------------------------------------------------------------
    public virtual List<string> GetCheckFields()  // проверка и выдача наименований полей
    {
      return Fields;
    }
    //-------------------------------------------------------------------------
    public virtual void GetData(TaskContext c, Action<bool> afterGetDataAction) // запуск получения данных
    {
      afterGetData = afterGetDataAction; // что вызвать по окончании (штатно - конце GetDataEnd())
      context = c ?? new TaskContext();
      context.Error = null;
      context.Cancel = false;
      if (context.OnFinish == null)
        context.OnFinish = GetDataEnd; // куда вернуться в конце, если мы запустим как процесс
      Parent.InProc = true;
      Parent.DTClear();
      Parent.DT = null;
    }
    //-------------------------------------------------------------------------
    public virtual void GetDataEnd(object result, string msg) // окончание получения данных
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
    public virtual void GetDataStop() { } // прервать получение данных (например если запущено как процесс)
    //-------------------------------------------------------------------------
    public virtual void GetDataError(string msg, Exception ex) // принять ошибку получения данных из процесса
    {
      context.Error = ex;
    }
  }
}

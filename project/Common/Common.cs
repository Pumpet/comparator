using System;
using System.IO;
using System.Threading;

namespace Common
{
  //===========================================================================
  /* общие процедуры */
  public static class CommonProc
  {
    public static string GetFilePath(string fileName)
    {
      return string.IsNullOrEmpty(fileName) ? fileName :
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileName(fileName)));
    }
  }
  //===========================================================================
  /* контекст выполнения процесса */
  public class TaskContext
  {
    public bool Cancel { get; set; } // признак прерывания
    public Exception Error { get; set; } // ошибка процесса
    public SynchronizationContext ViewContext { get; set; } // контекст синхронизации внешнего вью для выполнения методов отображения хода процесса
    public Action<int, string> OnProgress { get; set; } // отображение хода процесса (счетчик и сообщение)
    public Action<object, string> OnFinish { get; set; } // по завершении процесса (объект с данными и сообщение)
    public Action<string, Exception> OnError { get; set; } // при ошибке в процессе (сообщение и ошибка)
  }
  //===========================================================================
  /* интерфейс обработки ошибок и сообщений*/
  public interface ILoger
  {
    void Error(string mess, object objErr);
    void Message(string mess, bool critical); 
  }
}

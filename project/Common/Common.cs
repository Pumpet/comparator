using System;
using System.IO;
using System.Threading;

namespace Common
{
  //===========================================================================
  /* Common functions */
  public static class CommonProc
  {
    // Get path from relative path
    public static string GetFilePath(string fileName)
    {
      return string.IsNullOrEmpty(fileName) ? fileName :
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileName(fileName)));
    }
  }
  //===========================================================================
  /* Process execution context */
  public class TaskContext
  {
    public bool Cancel { get; set; } // interrupt flag
    public Exception Error { get; set; } // happened exception
    public SynchronizationContext ViewContext { get; set; } // synchronization context for displaying progress
    public Action<int, string> OnProgress { get; set; } // progress handler (counter, message)
    public Action<object, string> OnFinish { get; set; } // finish handler (data object, message)
    public Action<string, Exception> OnError { get; set; } // error handler (message, exception)
  }
  //===========================================================================
  /* Error and message handling interface */
  public interface ILoger
  {
    void Error(string mess, object objErr);
    void Message(string mess, bool critical); 
  }
}

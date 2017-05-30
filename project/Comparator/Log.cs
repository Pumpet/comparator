using System;
using System.Text;
using System.IO;

namespace Comparator
{
  public static class Log
  {
    private static object sync = new object();
    private static string fileName;
    //-------------------------------------------------------------------------
    /* Will be created with application path and application name + .log */
    static Log()
    {
      fileName = Path.ChangeExtension(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName), "log");
    }
    //-------------------------------------------------------------------------
    /* Log start or continue */
    public static void Start(bool rewrite) { Start(rewrite, null, null); }
    /* + start text */
    public static void Start(bool rewrite, string text) { Start(rewrite, text, null); }
    /* + specific log file name */
    public static void Start(bool rewrite, string text, string file)
    {
      if (string.IsNullOrEmpty(file)) file = fileName;
      if (!Path.IsPathRooted(file))
        file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file);
      string path = Path.GetDirectoryName(file);
      if (path != null && !Directory.Exists(path))
        Directory.CreateDirectory(path);
      fileName = file;
      if (rewrite)
        File.WriteAllText(fileName, string.Empty, Encoding.GetEncoding("Windows-1251"));
      if (text != null)
        Write(text, false);
    }
    //-------------------------------------------------------------------------
    /* Write error */
    public static void Write(string text, object ex) { Write(text, ex, false); }
    public static void Write(string text, object ex, bool echo)
    {
      if (ex == null)
        text = string.Format("ERROR {0}", text);
      else
      { 
        if (ex is Exception)
          text = string.Format("ERROR {0} [{1}.{2}()] {3}", text, ((Exception)ex).TargetSite.DeclaringType, ((Exception)ex).TargetSite.Name, ((Exception)ex).Message);
        else
          text = string.Format("ERROR {0} {1}", text, ex);
      }
      Write(text, echo);
    }
    /* Write message */
    public static void Write(string text) { Write(text, false); }
    public static void Write(string text, bool echo)
    {
      if (echo)
        Console.WriteLine(text);
      text = string.Format("[{0:dd.MM.yyy HH:mm:ss.fff}] {1}\r\n", DateTime.Now, text);
      try
      {
        lock (sync) { File.AppendAllText(fileName, text, Encoding.GetEncoding("Windows-1251")); }
      }
      catch { }
    }
  }
}

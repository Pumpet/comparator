using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Common
{
  //===========================================================================
  /* файл настроек */
  public class Config
  {
    static Config config;
    string file;
    public List<Options> optionsList = new List<Options>(); // настройки по хостам
    //-------------------------------------------------------------------------
    public void Init(string f)
    {
      config = this;
      file = f;
    }
    //-------------------------------------------------------------------------
    public static void Save()
    {
      Loader.Save(config.file, config);
    }
  }
  //===========================================================================
  /* настройки для хоста, запустившего приложение */
  public class Options
  {
    [XmlIgnore]
    public string DefPath;
    [XmlAttribute]
    public string Host = Dns.GetHostName();
    [XmlArrayItem("file")]
    public List<string> RecentFiles = new List<string>(); // список последних профилей
    public string ProfileFolder = ""; // каталог для профилей
    public string ResultFolder = ""; // каталог для результатов
    public string LogFile = ""; // лог пакетного режима
    public string HtmlStylesFile = ""; // css для результата в html
    public string PatternFile = ""; // профиль-шаблон для нового профиля
    public MailParams SendOptions;
    //-------------------------------------------------------------------------
    public static Options Create(bool batch)
    {
      string defPath = AppDomain.CurrentDomain.BaseDirectory;
      string configFileName = Path.Combine(defPath, Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName) + ".xml");

      Options opt;
      try
      {
        if (!File.Exists(configFileName))
          Loader.Save(configFileName, new Config());

        Config optList = Loader.Load<Config>(configFileName);
        opt = optList.optionsList.FirstOrDefault(x => x.Host == (batch ? "BATCH" : Dns.GetHostName()));
        if (opt == null)
        {
          opt = new Options();
          opt.Host = (batch ? "BATCH" : Dns.GetHostName());
          optList.optionsList.Add(opt);
        }
        optList.Init(configFileName);

        opt.DefPath = defPath;

        if (batch)
        {
          if (opt.SendOptions == null)
            opt.SendOptions = new MailParams();
          if (!string.IsNullOrEmpty(opt.SendOptions.Pwd))
          {
            opt.SendOptions.PwdEncr = opt.SendOptions.ed.Encrypt(opt.SendOptions.Pwd);
            opt.SendOptions.Pwd = "";
          }
        }

        if (!string.IsNullOrEmpty(opt.ProfileFolder))
          opt.ProfileFolder = Path.Combine(defPath, opt.ProfileFolder);
        if (string.IsNullOrEmpty(opt.ProfileFolder) || !Directory.Exists(opt.ProfileFolder))
          opt.ProfileFolder = Path.Combine(defPath, "profiles");
        if (!Directory.Exists(opt.ProfileFolder))
          Directory.CreateDirectory(opt.ProfileFolder);

        if (!string.IsNullOrEmpty(opt.ResultFolder))
          opt.ResultFolder = Path.Combine(defPath, opt.ResultFolder);
        if (string.IsNullOrEmpty(opt.ResultFolder) || !Directory.Exists(opt.ResultFolder))
          opt.ResultFolder = Path.Combine(defPath, "results");
        if (!Directory.Exists(opt.ResultFolder))
          Directory.CreateDirectory(opt.ResultFolder);

        if (string.IsNullOrEmpty(opt.LogFile))
          opt.LogFile = Path.Combine(defPath, Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName) + ".log");
        else
          opt.LogFile = Path.Combine(defPath, opt.LogFile);
        if (String.IsNullOrEmpty(Path.GetDirectoryName(opt.LogFile)) || !Directory.Exists(Path.GetDirectoryName(opt.LogFile)))
          opt.LogFile = Path.Combine(defPath, Path.GetFileName(opt.LogFile));

        if (string.IsNullOrEmpty(opt.HtmlStylesFile) || !File.Exists(opt.HtmlStylesFile))
          opt.HtmlStylesFile = Path.Combine(defPath, "styles.css");

        if (string.IsNullOrEmpty(opt.PatternFile) || !File.Exists(opt.PatternFile))
          opt.PatternFile = Path.Combine(defPath, "pattern.xml");

        Loader.Save(configFileName, optList);
      }
      catch (Exception ex)
      {
        string msg = String.Format("Configuration error!\n\nConfig file: {0}\n{1}", configFileName, ex);
        if (batch)
        {
          Console.WriteLine(msg);
          Console.ReadKey(true);
          Environment.Exit(1);
        }
        else
          MessageBox.Show(msg, "Configuration error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        opt = null;
      }
      return opt;
    }
  }
}

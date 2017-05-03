using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using DataComparer;
using System.IO.Compression;
using System.Text;
using Sources;
using Common;

namespace Comparator
{
  public class Master
  {
    bool batch;
    Profile profile;
    Options opt;
    IView view; // null в batchmode
    ILoger loger;
    TaskContext contextCompare;
    Thread taskCompare;
    CompareResult cmp;
    Dictionary<string,object> bindings = new Dictionary<string,object>();
    string profileFolder, resultFolder, dataFolder, logFile;
    bool openResult = true;
    //~~~~~~~~ Init ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    public Master(bool batchMode, IView v, string[] args)
    {
      batch = batchMode;
      opt = Options.Create(batch);
      if (opt == null) return;

      profileFolder = opt.ProfileFolder;
      resultFolder = opt.ResultFolder;
      dataFolder = opt.DefPath;

      string profileFile = (Array.Find(args, s => (s.IndexOf("-profile:") > -1)) ?? "").Replace("-profile:", "");

      if (batch)
        StartBatchMode(args, profileFile);
      else
        StartViewMode(v, profileFile);
    }
    //-------------------------------------------------------------------------
    /* старт (batchmode) */
    void StartBatchMode(string[] args, string profileFile)
    {
      // лог
      logFile = opt.LogFile;
      string log = (Array.Find(args, s => (s.IndexOf("-log:") > -1)) ?? "").Replace("-log:", "").Trim().ToLower();
      if (!string.IsNullOrEmpty(log))
        logFile = log;
      loger = new Loger(logFile);
      OnMessage(">>> Start process for \"" + profileFile + "\"");

      try
      {
        // профиль
        if (!string.IsNullOrEmpty(profileFile))
          profileFile = Path.Combine(profileFolder, profileFile);
        if (string.IsNullOrEmpty(profileFile) || !File.Exists(profileFile))
          throw new Exception("Profile not exists");
        profile = Loader.Load<Profile>(profileFile);
        if (profile == null)
          throw new Exception("Error load profile");
        OnMessage("Profile loaded");

        // тип результата
        string t = (Array.Find(args, s => (s.IndexOf("-type:") > -1)) ?? "").Replace("-type:", "").Trim().ToLower();
        if (t == "excel") profile.ResType = ViewResultType.Excel;
        if (t == "html") profile.ResType = ViewResultType.HTML;
      
        // каталог результата
        if (!string.IsNullOrEmpty(profile.ResFolder))
          resultFolder = Path.Combine(opt.DefPath, profile.ResFolder);
        string path = (Array.Find(args, s => (s.IndexOf("-path:") > -1)) ?? "").Replace("-path:", "").Trim();
        if (!string.IsNullOrEmpty(path))
          resultFolder = Path.Combine(opt.DefPath, path);
        if (!Directory.Exists(resultFolder))
          throw new Exception("Result folder not exists: \"" + resultFolder + "\"");
        
        // файл результата
        string file = (Array.Find(args, s => (s.IndexOf("-file:") > -1)) ?? "").Replace("-file:", "").Trim();
        if (!string.IsNullOrEmpty(file))
          profile.ResFile = file;
        if (string.IsNullOrEmpty(profile.ResFile))
          profile.ResFile = Path.ChangeExtension(profileFile, profile.ResType == ViewResultType.Excel ? "xls" : "htm");
        profile.ResFile = Path.GetFileName(profile.ResFile);
        
        // параметры рассылки
        string sendto = Array.Find(args, s => (s.IndexOf("-sendto:") > -1));
        if (sendto != null)
        {
          sendto = sendto.Replace("-sendto:", "").Trim();
          profile.Send = (sendto != "");
          profile.SendTo = sendto;
        }
        // 
        openResult = Array.Exists(args, s => (s.ToLower() == "-open"));
      }
      catch (Exception ex)
      {
        OnError(ex.Message, ex);
        return;
      }
      
      // запуск сверки (выход будет по окончании процесса или после выдачи ошибки)
      ComparePrepare(null, null, null);
    }
    //-------------------------------------------------------------------------
    /* старт (winmode) */
    void StartViewMode(IView v, string profileFile)
    {
      view = v;
      if (view is ILoger)
        loger = (ILoger)view;
      // подписка на вью
      view.NewProfile += NewProfile;
      view.LoadProfile += LoadProfile;
      view.SaveProfile += SaveProfile;
      view.Check += Check;
      view.Compare += ComparePrepare;
      view.CompareStop += CompareStop;
      view.Result += CompareResult;
      view.DataEdit += DataEdit;
      view.CloseView += Close;
      // подписка на вью (события формирования списка полей и их пар)
      view.FillColPairs += FillColPairs;
      view.RemoveColPair += RemoveColPair;
      view.MoveColPair += MoveColPair;
      view.GetFields += GetFields;
      // подписка на вью (события источников)
      view.viewSourceA.Command += CommandSourceA;
      view.viewSourceB.Command += CommandSourceB;
      // начальные установки вью
      InitDataBinding();
      SetRecentFiles();

      if (!string.IsNullOrEmpty(profileFile))
        LoadProfile(Path.Combine(profileFolder, profileFile));
      if (profile == null)
      {
        profile = new Profile();
        NewProfile(false);
      }
    }
    //-------------------------------------------------------------------------
    /* инициализация привязок ко вью (winmode) */
    private void InitDataBinding()
    {
      // имена объектов биндинга для вью (сам объект будет задан в SetViewData)
      bindings.Add("Profile", null);
      bindings.Add("Cols", null);
      bindings.Add("SourceA", null);
      bindings.Add("SourceB", null);
      bindings.Add("SourceTypeA", null);
      bindings.Add("SourceTypeB", null);
      bindings.Add("SheetsA", null);
      bindings.Add("SheetsB", null); 
      bindings.Add("DbSourceA", null);
      bindings.Add("DbSourceB", null);
      bindings.Add("ExcelSourceA", null);
      bindings.Add("ExcelSourceB", null);
      bindings.Add("CsvSourceA", null);
      bindings.Add("CsvSourceB", null);
      bindings.Add("XmlSourceA", null);
      bindings.Add("XmlSourceB", null);

      // соответсвие полей биндинга для вью и полей модели
      Dictionary<string, string> pn = new Dictionary<string, string>();
      pn.Add("ViewCaption", "ViewCaption");
      pn.Add("DiffOnly", "DiffOnly");
      pn.Add("OnlyA", "OnlyA");
      pn.Add("OnlyB", "OnlyB");
      pn.Add("Send", "Send");
      pn.Add("SendTo", "SendTo");
      pn.Add("ResExcel", "ResExcel");
      pn.Add("ResHTML", "ResHTML");
      pn.Add("ResFolder", "ResFolder");
      pn.Add("ResFile", "ResFile");
      pn.Add("TimeInResFile", "TimeInResFile");
      pn.Add("ResMail", "ResMail");
      pn.Add("Subject", "Subject");
      pn.Add("src.Name", "Name");
      pn.Add("src.SrcType", "SrcType");
      pn.Add("MatchInOrder", "MatchInOrder");
      pn.Add("MatchAllPairs", "MatchAllPairs");
      pn.Add("CheckRepeats", "CheckRepeats");
      pn.Add("TryConvert", "TryConvert");
      pn.Add("NullAsStr", "NullAsStr");
      pn.Add("CaseSens", "CaseSens");
      pn.Add("cols.Key", "Key");
      pn.Add("cols.Match", "Match");
      pn.Add("cols.ColA", "ColA");
      pn.Add("cols.ColB", "ColB");
      pn.Add("src.File","Filename");
      pn.Add("src.RngStart", "RngStart");
      pn.Add("src.RngEnd", "RngEnd");
      pn.Add("src.Sheet", "Sheet");
      pn.Add("src.FirstLineNames", "FirstLineNames");
      pn.Add("src.Delimiter", "Delimiter");
      pn.Add("src.Codepage", "Codepage");
      pn.Add("src.ConnectionInfo", "ConnectionInfo");
      pn.Add("src.XmlInfo", "Info");
      view.SetDataProps(pn);
    }
    //-------------------------------------------------------------------------
    /* список последних загруженных профилей (winmode) */
    private void SetRecentFiles(string file = null)
    {
      if (!string.IsNullOrEmpty(file))
      {
        int idx = opt.RecentFiles.IndexOf(file);
        if (idx >= 0) opt.RecentFiles.RemoveAt(idx);
        opt.RecentFiles.Insert(0, file);
        if (opt.RecentFiles.Count > 5) opt.RecentFiles.RemoveAt(opt.RecentFiles.Count - 1);
      }
      view.RecentFiles = opt.RecentFiles; // список последних профилей для меню вью
    }
    //-------------------------------------------------------------------------
    /* передача данных во вью (winmode) */
    private void SetViewData()
    {
      if (profile.SrcA.DbSource != null) // для источников-БД подцепляем редактор
        view.viewSourceA.SqlView = profile.SrcA.DbSource.GetSqlView();
      if (profile.SrcB.DbSource != null)
        view.viewSourceB.SqlView = profile.SrcB.DbSource.GetSqlView();
      bindings["Profile"] = profile;
      bindings["MailResult"] = Enum.GetValues(typeof(MailResult));
      bindings["Cols"] = profile.Cols;
      bindings["SourceA"] = profile.SrcA;
      bindings["SourceB"] = profile.SrcB;
      bindings["DbSourceA"] = profile.SrcA.DbSource;
      bindings["DbSourceB"] = profile.SrcB.DbSource;
      bindings["ExcelSourceA"] = profile.SrcA.ExcelSource;
      bindings["ExcelSourceB"] = profile.SrcB.ExcelSource;
      bindings["CsvSourceA"] = profile.SrcA.CsvSource;
      bindings["CsvSourceB"] = profile.SrcB.CsvSource;
      bindings["XmlSourceA"] = profile.SrcA.XmlSource;
      bindings["XmlSourceB"] = profile.SrcB.XmlSource;
      bindings["SourceTypeA"] = Enum.GetValues(typeof(SourceType));
      bindings["SourceTypeB"] = Enum.GetValues(typeof(SourceType));
      bindings["SheetsA"] = profile.SrcA.ExcelSource.Sheets;
      bindings["SheetsB"] = profile.SrcB.ExcelSource.Sheets;
      view.SetData(bindings);
    }
    #endregion
    //~~~~~~~~ Handlers (winmode) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    public void NewProfile(bool copy)
    {
      try
      {
        if (profile != null && profile.needSave)
        {
          string req = view.SaveRequest();
          if (req == "Cancel") return;
          if (req == "Yes" && !SaveProfile()) return;
        }

        if (profile != null && !copy)  // это новый, а не копия открытого
        {
          profile.Dispose();
          profile = null;
          GC.Collect();
        }
        ClearResult();

        if (!copy)
        {
          if (File.Exists(opt.PatternFile)) // новый профиль - из шаблона
            profile = Loader.Load<Profile>(opt.PatternFile);
          else
            profile = new Profile();
        }
        profile.Filepath = string.Empty;
        profile.needSave = false;
        SetViewData();
        OnMessage("New profile");
      }
      catch (Exception ex)
      {
        OnError("Error create new profile", ex);
      }
    }
    //-------------------------------------------------------------------------
    public void LoadProfile(string file)
    {
      Profile tmp = profile;
      try
      {
        if (profile != null && profile.needSave)
        {
          string req = view.SaveRequest();
          if (req == "Cancel") return;
          if (req == "Yes" && !SaveProfile()) return;
        }

        if (!File.Exists(file))
          file = view.LoadFile(profileFolder, @"XML|*.xml|ALL|*.*", "xml");
        if (string.IsNullOrEmpty(file)) return;

        if (profile != null)
        {
          profile.Dispose();
          profile = null;
          GC.Collect();
        }
        ClearResult();
        profile = Loader.Load<Profile>(file);
        profile.Filepath = file;
        profileFolder = Path.GetDirectoryName(file);
        SetRecentFiles(file);
        profile.needSave = false;
        SetViewData();
        OnMessage("Profile loaded");
      }
      catch (Exception ex)
      {
        profile = tmp;
        OnError("Error load profile", ex);
      }
    }
    //-------------------------------------------------------------------------
    public bool SaveProfile()
    {
      string file = profile.Filepath;
      try
      {
        profile.Prepare();
        if (!File.Exists(file))
          file = view.SaveFile(profileFolder, profile.ProfileName, @"XML|*.xml", "xml");
        if (string.IsNullOrEmpty(file)) return false;
        Loader.Save(file, profile);
        profile = Loader.Load<Profile>(file);
        profile.Filepath = file;
        profileFolder = Path.GetDirectoryName(file);
        SetRecentFiles(file);
        profile.needSave = false;
        SetViewData();
        OnMessage("Profile saved");
        return true;
      }
      catch (Exception ex)
      {
        OnError("Error save profile", ex);
        return false;
      }
    }
    //-------------------------------------------------------------------------
    public bool Close()
    {
      if (profile.needSave)
      {
        string req = view.SaveRequest();
        if (req == "Cancel") return false;
        if (req == "Yes" && !SaveProfile()) return false;
      }
      opt.ProfileFolder = profileFolder;
      Config.Save();
      ClearResult();
      return true;
    }
    //-------------------------------------------------------------------------
    public void DataEdit()
    {
      profile.needSave = true;
      OnMessage("Profile data changed");
    }
    #endregion
    //~~~~~~~~ Check and Compare ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    /* проверка профиля и проверка пар */
    private bool Check()
    {
      try
      {
        profile.Check();
        profile.CheckFieldPairs(true);
        OnMessage("Checked");
        return true;
      }
      catch (Exception ex)
      {
        OnError("Check profile error", ex);
        return false;
      }
    }
    //-------------------------------------------------------------------------
    /* подготовка к запуску сверки, проверки и запуск получения данных источников */
    /* получаем контексты для процессов получения данных источника A, источника B и процесса сверки */
    void ComparePrepare(TaskContext contextA, TaskContext contextB, TaskContext context)
    {
      // уточнение контекстов процессов получения:
      if (contextA == null) contextA = new TaskContext();
      if (contextB == null) contextB = new TaskContext();
      if (contextA.OnError == null) contextA.OnError = profile.SrcA.Content.GetDataError; // отражение ошибок нужно всегда
      if (contextB.OnError == null) contextB.OnError = profile.SrcB.Content.GetDataError;
      // если OnFinish не задан - он по умолчанию = SourceContent.GetDataEnd и задается внутри SourceContent.GetData
      //
      // уточнение контекстов процесса сверки:
      if (context == null) context = new TaskContext();
      if (context.OnError == null) context.OnError = OnError;
      contextCompare = context;
      //
      try
      {
        profile.Prepare();
        profile.Check(); // только проверка профиля, а проверка пар - после получения данных источников, чтобы иметь последние списки полей
        ClearResult();
        profile.SrcA.InProc = true;
        profile.SrcB.InProc = true;
        // запуск процессов получения данных источников, с последующим вызовом CompareExec()
        profile.SrcA.Content.GetData(contextA, CompareExec); 
        profile.SrcB.Content.GetData(contextB, CompareExec);
      }
      catch (Exception ex)
      {
        OnError("Prepare data error", ex);
        CompareStop();
      }
    }
    //-------------------------------------------------------------------------
    /* сюда возвращаемся после окончания/прерывания процессов получения данных источников */
    /* и запускаем процесс сверки в случае корректного окончания получения данных и проверки пар */
    void CompareExec(bool stop)
    {
      if (stop) // процесс получения данных, из которого вернулись, был прерван (об ошибке или по желанию) 
      {
        if (!contextCompare.Cancel) // нужно прервать процесс сверки, если она еще не прерывалась
          CompareStop();  
        return;
      }
      if (profile.SrcA.InProc || profile.SrcB.InProc)
        return; // ждем другой процесс получения данных
      try
      {
        profile.CheckFieldPairs(false); // проверка пар по свежеполученным данным
        contextCompare.Cancel = false;
        contextCompare.Error = null;
        taskCompare = new Thread(CompareTask);
        taskCompare.Name = "TaskCompare";
        taskCompare.IsBackground = true;
        taskCompare.Start(); // запуск процесса сверки
      }
      catch (Exception ex)
      {
        OnError("Compare execution error", ex);
        CompareStop();
      }
    }
    //-------------------------------------------------------------------------
    /* внутри процесса сверки */
    void CompareTask()
    {
      try
      {
        MatchType kmt = profile.MatchInOrder ? MatchType.NoMatch : MatchType.Selected;
        MatchType cmt = profile.MatchAllPairs ? MatchType.All : MatchType.Selected;
        // сверка и получение результата
        cmp = DSComparer.Compare(new[] { profile.SrcA.DT, profile.SrcB.DT }, kmt, cmt, 
          profile.CheckRepeats, profile.TryConvert, profile.NullAsStr, profile.CaseSens,
          profile.DiffOnly, profile.OnlyA, profile.OnlyB,          
          new[] { profile.Cols.Select(x => x.ColA), profile.Cols.Select(x => x.ColB) },
          new[] { profile.Cols.Where(x => x.Key).Select(x => x.ColA), profile.Cols.Where(x => x.Key).Select(x => x.ColB) },
          new[] { profile.Cols.Where(x => x.Match).Select(x => x.ColA), profile.Cols.Where(x => x.Match).Select(x => x.ColB) },
          (x) => {
            if (contextCompare.ViewContext != null)
              contextCompare.ViewContext.Send(CompareProgress, x); // ход процесса сверки - выполняем в контексте внешнего вью
            else
              CompareProgress(x);
          });

        if (contextCompare.ViewContext != null)
          contextCompare.ViewContext.Send(CompareFinish, true); // действия по окончании сверки - выполняем в контексте внешнего вью
        else
          CompareFinish(true);
      }
      catch (ThreadAbortException) // срубили процесс
      { }
      catch (Exception ex)
      {
        contextCompare.Error = ex;
        if (contextCompare.ViewContext != null)
          contextCompare.ViewContext.Send(CompareFinish, false);
        else
          CompareFinish(false);
      }
    }
    //-------------------------------------------------------------------------
    /* ход процесса сверки */
    void CompareProgress(object step) // step = Tuple<int=шаг, string=сообщение>
    {
      if (contextCompare.OnProgress != null)
        contextCompare.OnProgress((step as Tuple<int, string>).Item1, "Compare: " + (step as Tuple<int, string>).Item2);
    }
    //-------------------------------------------------------------------------
    /* остановка процесса сверки или получения данных - в случае ошибок или по желанию */
    void CompareStop()
    {
      contextCompare.Cancel = true;
      bool errA = (profile.SrcA.Content.Error != null),
        errB = (profile.SrcB.Content.Error != null);

      // принудительное завершение безошибочно работающего процесса получения данных
      if (profile.SrcA.InProc && !errA)
        profile.SrcA.Content.GetDataStop();
      if (profile.SrcB.InProc && !errB)
        profile.SrcB.Content.GetDataStop();

      if (errA)
        contextCompare.OnError("Error in " + profile.SrcA.Name, profile.SrcA.Content.Error);
      if (errB)
        contextCompare.OnError("Error in " + profile.SrcB.Name, profile.SrcB.Content.Error);

      bool closeProgressView = !errA && !errB; // нужно или нет закрыть окно прогресса сверки
      if (taskCompare != null && taskCompare.IsAlive) // уже идет процесс сверки
      {
        taskCompare.Abort(); // срубать так - проще
        taskCompare.Join(0);
        ClearResult();
        CompareFinish(closeProgressView);
      }
      else if (contextCompare.OnFinish != null) // еще не идет процесс сверки
        contextCompare.OnFinish(closeProgressView, null); // подвесим или закроем окошко прогресса в зависимости от closeProgressView
    }
    //-------------------------------------------------------------------------
    /* завершение процесса сверки - успешно, в случае ошибок или по желанию */
    void CompareFinish(object closeProgressView)
    {
      string msg = "Compared";
      bool fail = false;
      if (contextCompare.Cancel)
      {
        msg = "Compare stopped";
        fail = true;
      }
      if (contextCompare.Error != null)
      {
        msg = "Compare stopped on error";
        fail = true;
        contextCompare.OnError("Compare error", contextCompare.Error);
      }
      if (contextCompare.OnProgress != null)
        contextCompare.OnProgress(fail ? 0 : int.MaxValue, msg);

      OnMessage(msg);
      if (!fail) CompareResult(); // покажем результат если все ок
      if (contextCompare.OnFinish != null)
        contextCompare.OnFinish((bool)closeProgressView, null); // подвесим или закроем окошко прогресса в зависимости от closeProgressView
    }
    //-------------------------------------------------------------------------
    /* выдача результата */
    void CompareResult()
    {
      try
      {
        if (batch)
        {
          if (cmp == null)
            throw new Exception("No compare results");
          if (profile.TimeInResFile)
            profile.ResFile = string.Concat(Path.GetFileNameWithoutExtension(profile.ResFile), DateTime.Now.ToString("__yyyyMMdd_HHmmss"), Path.GetExtension(profile.ResFile));
          string resultFile = cmp.ToHTML(openResult, (profile.ResType == ViewResultType.Excel), resultFolder, profile.ResFile, opt.HtmlStylesFile, OnError);
          if (!string.IsNullOrEmpty(resultFile) && File.Exists(resultFile))
          {
            OnMessage("Save result in \"" + resultFile + "\"");
            SendResult(resultFile);
          }
          else
            throw new Exception("Result file not found in " + resultFile);
          Environment.Exit(0);
        }
        else
        {
          view.Compared = (cmp != null);
          if (cmp == null) return;
          view.WaitResult(true, "Prepare result output... Please wait.");
          view.ResultHWnd = IntPtr.Zero;
          view.ResultHWnd = cmp.OpenForm(resultFolder, null, opt.HtmlStylesFile);
        }
      }
      catch (Exception ex)
      {
        OnError("Error result output", ex);
      }
      finally
      {
        if (!batch) 
          view.WaitResult(false);
      }
    }
    //-------------------------------------------------------------------------
    /* отправка результата */
    void SendResult(string resultFile)
    {
      if (!profile.Send || string.IsNullOrEmpty(profile.SendTo.Trim()))
        return;
      try
      {
        string body = "";
        string[] attachFiles = null;
        string subject = string.IsNullOrEmpty(profile.Subject) ? string.Format("Compare {0} and {1}", cmp.NameA, cmp.NameB) : profile.Subject;

        switch (profile.ResMail)
        {
          case MailResult.Attachment:
            body = cmp.HtmlSummary().Replace("</BODY>", "<p>See result details in attachment</p></BODY>");
            attachFiles = new[] {resultFile};
            break;
          case MailResult.GZIP:
            body = cmp.HtmlSummary().Replace("</BODY>", "<p>See result details in attachment archive</p></BODY>");
            using (FileStream fromFS = new FileStream(resultFile, FileMode.Open))
            {
              resultFile = resultFile + ".gz";
              using (FileStream toFS = File.Create(resultFile))
              {
                using (GZipStream zip = new GZipStream(toFS, CompressionMode.Compress))
                {
                  fromFS.CopyTo(zip);
                }
              }
            }
            OnMessage("Compress result file to \"" + resultFile + "\"");
            attachFiles = new[] { resultFile };
            break;
          case MailResult.Text:
            body = File.ReadAllText(resultFile, Encoding.GetEncoding(1251));
            break;
          case MailResult.Link:
            body = cmp.HtmlSummary()
              .Replace("</BODY>", string.Format("<p>See result details: <a href=\"{0}\">{1}</a></p></BODY>", resultFile, resultFile));
            break;
          default:
            break;
        }
        Mailer.SendMail(opt.SendOptions, profile.SendTo, subject, body, attachFiles);
        OnMessage("Send result to " + profile.SendTo);
      }
      catch (Exception ex)
      {
        OnError("Error send result", ex); 
      }
    }
    //-------------------------------------------------------------------------
    /* очистка результатов сверки - бережем память */
    void ClearResult()
    {
      if (cmp != null)
      {
        cmp.Dispose();
        cmp = null;
        GC.Collect();
      }
      if (view != null)
      {
        view.Compared = false;
        view.ResultHWnd = IntPtr.Zero;
      }
    }
    #endregion
    //~~~~~~~~ Fields handlers (winmode) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    /* пары по спискам полей */
    void FillColPairs(bool clear, bool key, bool match, List<string> listA, List<string> listB)
    {
      if (key) match = true;
      if (profile.Cols == null)
        profile.Cols = new List<ColPair>();
      if (clear) profile.Cols.Clear();

      Action<List<string>, List<string>, bool> insert = (listMax, listMin, maxIsA) =>
      {
        for (int i = 0; i < listMax.Count; i++)
        {
          string a = string.IsNullOrEmpty(listMax[i]) ? string.Empty : listMax[i];
          string b = listMin.Count < i + 1 || string.IsNullOrEmpty(listMin[i]) ? string.Empty : listMin[i];
          if (a == string.Empty || b == string.Empty) { key = false; match = false; }
          profile.Cols.Add(new ColPair() { Key = key, Match = match, ColA = maxIsA ? a : b, ColB = maxIsA ? b : a });
        }
      };

      if (listA.Count > listB.Count)
        insert(listA, listB, true);
      else
        insert(listB, listA, false);
    }
    //-------------------------------------------------------------------------
    /* удаление пар с указанными индексами */
    void RemoveColPair(int[] idxs)
    {
      if (profile.Cols == null) return;
      foreach (ColPair r in idxs.Select(x => profile.Cols[x]).ToList())
        profile.Cols.Remove(r);
    }
    //-------------------------------------------------------------------------
    /* перемещение пар с указанными индексами */
    int MoveColPair(int[] idxs, int offset)
    {
      if (profile.Cols == null || (offset == -1 && idxs.Min() == 0) || (offset == 1 && idxs.Max() == profile.Cols.Count - 1))
        return -1;
      List<ColPair> pairs;
      if (offset == -1)
        pairs = idxs.OrderBy(x => x).Select(x => profile.Cols[x]).ToList();
      else
        pairs = idxs.OrderByDescending(x => x).Select(x => profile.Cols[x]).ToList();

      foreach (ColPair r in pairs)
      {
        int idx = profile.Cols.FindIndex(x => x == r);
        profile.Cols.Remove(r);
        profile.Cols.Insert(idx + offset, r);
      }

      return idxs.Min() + offset; // куда переместился текущий индекс
    }
    //-------------------------------------------------------------------------
    /* списки имен полей источников, за исключением имеющихся в парах */
    bool GetFields(List<string> listA, List<string> listB)
    {
      try
      {
        List<string>[] flds = profile.GetFields(true);

        if (profile.Cols != null)
        {
          listA.AddRange(flds[0].Except(profile.Cols.Select(x => x.ColA)).Distinct());
          listB.AddRange(flds[1].Except(profile.Cols.Select(x => x.ColB)).Distinct());
        }
        else
        {
          listA.AddRange(flds[0]);
          listB.AddRange(flds[1]);
        }

        return true;
      }
      catch (Exception ex)
      {
        OnError("Error get fields", ex);
        return false;
      }
    }
    #endregion
    //~~~~~~~~ Source commands (winmode) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    object CommandSourceA(string command, object param) // обработка команды от вью источника A
    {
      object ret = CommandSource(profile.SrcA, command, param);
      switch (command)
      {
        case "SelectFileCsv":
          view.RefreshData(null, "CsvSourceA");
          break;
        case "SelectFileExcel":
          view.RefreshData(null, "ExcelSourceA");
          break;
        case "SetExcelSeets":
          bindings["SheetsA"] = profile.SrcA.ExcelSource.Sheets;
          view.RefreshData(bindings, "SheetsA", "ExcelSourceA");
          break;
        default:
          break;
      }
      return ret;
    }
    //-------------------------------------------------------------------------
    object CommandSourceB(string command, object param) // обработка команды от вью источника B
    {
      object ret = CommandSource(profile.SrcB, command, param);
      switch (command)
      {
        case "SelectFileCsv":
          view.RefreshData(null, "CsvSourceB");
          break;
        case "SelectFileExcel":
          view.RefreshData(null, "ExcelSourceB");
          break;
        case "SetExcelSeets":
          bindings["SheetsB"] = profile.SrcB.ExcelSource.Sheets;
          view.RefreshData(bindings, "SheetsB", "ExcelSourceB");
          break;
        default:
          break;
      }
      return ret;
    }
    //-------------------------------------------------------------------------
    object CommandSource(Source src, string command, object param) // обработка команды от вью источника (общий случай)
    {
      object ret = null;
      try
      {
        switch (command)
        {
          case "SelectFileCsv":
            src.CsvSource.LoadCsv(view.LoadFile(dataFolder, @"CSV|*.csv;*.txt|ALL|*.*", "csv"));
            if (File.Exists(CommonProc.GetFilePath(src.CsvSource.Filename)))
              dataFolder = Path.GetDirectoryName(CommonProc.GetFilePath(src.CsvSource.Filename));
            break;
          case "ViewDataCsv":
            src.CsvSource.GetData(null,null);
            ret = Tuple.Create<object, string, string>(src.DT, src.CsvSource.Filename, string.Format("{0} rows", src.DT == null ? 0 : src.DT.Rows.Count));
            break;
          case "SelectFileExcel":
            string fn = view.LoadFile(dataFolder, @"Excel|*.xl*|ALL|*.*", "xls");
            ret = (!string.IsNullOrEmpty(fn) && File.Exists(fn));
            if ((bool)ret)
            {
              src.ExcelSource.Filename = fn;
              dataFolder = Path.GetDirectoryName(fn);
            }
            break;
          case "SetExcelSeets":
            src.ExcelSource.Sheets = (param as Tuple<List<string>, string>).Item1;
            src.ExcelSource.Sheet = (param as Tuple<List<string>, string>).Item2;
            break;
          case "ViewDataExcel":
            src.ExcelSource.GetData(null, null);
            ret = Tuple.Create<object, string, string>(src.DT, src.ExcelSource.Filename, string.Format("{0} rows", src.DT == null ? 0 : src.DT.Rows.Count));
            break;
          case "RunXmlOptionsPrepare":
            ret = Tuple.Create<object, string>(src.XmlSource, "Xml Options for " + src.Name);
            break;
          case "GetXmlFields":
            src.XmlSource.GetCheckFields();
            break;
          default:
            break;
        }
      }
      catch (Exception ex)
      {
        OnError(string.Format("Error process command \"{0}\" with source \"{1}\"", command, src.Name), ex);
      }
      return ret;
    }
    #endregion
    //~~~~~~~~ Loger ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    private void OnError(string mess, Exception ex)
    {
      if (loger != null)
        loger.Error(mess, ex);
    }
    //-------------------------------------------------------------------------
    private void OnMessage(string mess, bool critical = false)
    {
      if (loger != null)
        loger.Message(mess, critical);
    }
    #endregion
  }
}

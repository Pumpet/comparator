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
    IView view; // null in batchmode
    ILoger loger;
    TaskContext contextCompare; // parameters for comparison process
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
    /* Start (batchmode) */
    void StartBatchMode(string[] args, string profileFile)
    {
      // start log
      logFile = opt.LogFile;
      string log = (Array.Find(args, s => (s.IndexOf("-log:") > -1)) ?? "").Replace("-log:", "").Trim().ToLower();
      if (!string.IsNullOrEmpty(log))
        logFile = log;
      loger = new Loger(logFile);
      OnMessage(">>> Start process for \"" + profileFile + "\"");

      try
      {
        // load profile
        if (!string.IsNullOrEmpty(profileFile))
          profileFile = Path.Combine(profileFolder, profileFile);
        if (string.IsNullOrEmpty(profileFile) || !File.Exists(profileFile))
          throw new Exception("Profile not exists");
        profile = Loader.Load<Profile>(profileFile);
        if (profile == null)
          throw new Exception("Error load profile");
        OnMessage("Profile loaded");

        // set result type
        string t = (Array.Find(args, s => (s.IndexOf("-type:") > -1)) ?? "").Replace("-type:", "").Trim().ToLower();
        if (t == "excel") profile.ResType = ViewResultType.Excel;
        if (t == "html") profile.ResType = ViewResultType.HTML;
      
        // set result folder
        if (!string.IsNullOrEmpty(profile.ResFolder))
          resultFolder = Path.Combine(opt.DefPath, profile.ResFolder);
        string path = (Array.Find(args, s => (s.IndexOf("-path:") > -1)) ?? "").Replace("-path:", "").Trim();
        if (!string.IsNullOrEmpty(path))
          resultFolder = Path.Combine(opt.DefPath, path);
        if (!Directory.Exists(resultFolder))
          throw new Exception("Result folder not exists: \"" + resultFolder + "\"");
        
        // set result file name
        string file = (Array.Find(args, s => (s.IndexOf("-file:") > -1)) ?? "").Replace("-file:", "").Trim();
        if (!string.IsNullOrEmpty(file))
          profile.ResFile = file;
        if (string.IsNullOrEmpty(profile.ResFile))
          profile.ResFile = Path.ChangeExtension(profileFile, profile.ResType == ViewResultType.Excel ? "xls" : "htm");
        profile.ResFile = Path.GetFileName(profile.ResFile);
        
        // set mailing params
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
      
      // start comparison process
      ComparePrepare(null, null, null);
    }
    //-------------------------------------------------------------------------
    /* Start (winmode) */
    void StartViewMode(IView v, string profileFile)
    {
      view = v;
      if (view is ILoger)
        loger = (ILoger)view;

      // subscription to events
      // for profile
      view.NewProfile += NewProfile;
      view.LoadProfile += LoadProfile;
      view.SaveProfile += SaveProfile;
      view.CloseView += Close;
      view.DataEdit += DataEdit;
      view.Check += Check;
      // for comparison process
      view.Compare += ComparePrepare;
      view.CompareStop += CompareStop;
      view.Result += CompareResult;
      // for fields and pairs
      view.FillColPairs += FillColPairs;
      view.RemoveColPair += RemoveColPair;
      view.MoveColPair += MoveColPair;
      view.GetFields += GetFields;
      // for sources
      view.viewSourceA.Command += CommandSourceA;
      view.viewSourceB.Command += CommandSourceB;
      
      // start settings for view
      InitDataBinding();
      SetRecentFiles();

      // load profile
      if (!string.IsNullOrEmpty(profileFile))
        LoadProfile(Path.Combine(profileFolder, profileFile));
      if (profile == null)
      {
        profile = new Profile();
        NewProfile(false);
      }
    }
    //-------------------------------------------------------------------------
    /* Init bindings (winmode) */
    private void InitDataBinding()
    {
      // set keys for binding objects to be defined in SetViewData
      bindings.Add("Profile", null);
      bindings.Add("MailResult", null);
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


      // key = "binding name" to be used in view, value = binding object's property name 
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
    /* set list of last loaded profiles (winmode) */
    private void SetRecentFiles(string file = null)
    {
      if (!string.IsNullOrEmpty(file))
      {
        int idx = opt.RecentFiles.IndexOf(file);
        if (idx >= 0) opt.RecentFiles.RemoveAt(idx);
        opt.RecentFiles.Insert(0, file);
        if (opt.RecentFiles.Count > 5) opt.RecentFiles.RemoveAt(opt.RecentFiles.Count - 1);
      }
      view.RecentFiles = opt.RecentFiles; 
    }
    //-------------------------------------------------------------------------
    /* Define and send data to view (winmode) */
    private void SetViewData()
    {
      // set code editor view for sql-source
      if (profile.SrcA.DbSource != null) 
        view.viewSourceA.SqlView = profile.SrcA.DbSource.GetSqlView();
      if (profile.SrcB.DbSource != null)
        view.viewSourceB.SqlView = profile.SrcB.DbSource.GetSqlView();
      // define objects for binding
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
      view.SetData(bindings); // send to BindingSources in view
    }
    #endregion
    //~~~~~~~~ Profile handlers (winmode) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    /* Create or copy new profile */
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

        // drop old data
        if (profile != null && !copy)  
        {
          profile.Dispose();
          profile = null;
          GC.Collect();
        }
        ClearResult();

        if (!copy)
        {
          if (File.Exists(opt.PatternFile)) // get new profile from pattern
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
    /* Load profile from file */
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

        // drop old data
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
    /* Save profile to file */
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
    /* Attempt to close work */
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
    /* Profile data has changed */
    public void DataEdit()
    {
      profile.needSave = true;
      OnMessage("Profile data changed");
    }
    //-------------------------------------------------------------------------
    /* Check profile */
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
    #endregion
    //~~~~~~~~ Comparison process ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    /* Prepare to start, check and get data from sources, and then - start comparison */
    void ComparePrepare(TaskContext contextA, TaskContext contextB, TaskContext context)
    {
      // prepare contexts for getting data processes
      if (contextA == null) contextA = new TaskContext();
      if (contextB == null) contextB = new TaskContext();
      if (contextA.OnError == null) contextA.OnError = profile.SrcA.Content.GetDataError; 
      if (contextB.OnError == null) contextB.OnError = profile.SrcB.Content.GetDataError;

      // prepare context for comparison process
      if (context == null) context = new TaskContext();
      if (context.OnError == null) context.OnError = OnError;
      contextCompare = context;
      
      try
      {
        profile.Prepare();
        profile.Check(); 
        ClearResult();
        profile.SrcA.InProc = true;
        profile.SrcB.InProc = true;
        // get data from source, and then - CompareExec()
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
    /* Action after get data process finished or stopped */
    /* Start comparison process after all data get and checked */
    void CompareExec(bool stop)
    {
      if (stop) // get data process stopped
      {
        if (!contextCompare.Cancel) // need to stop all processes
          CompareStop();  
        return;
      }
      if (profile.SrcA.InProc || profile.SrcB.InProc)
        return; // wait while another get data process finished or stopped
      try
      {
        profile.CheckFieldPairs(false); // check fiels pairs for just getting data
        contextCompare.Cancel = false;
        contextCompare.Error = null;
        taskCompare = new Thread(CompareTask);
        taskCompare.Name = "TaskCompare";
        taskCompare.IsBackground = true;
        taskCompare.Start();
      }
      catch (Exception ex)
      {
        OnError("Compare execution error", ex);
        CompareStop();
      }
    }
    //-------------------------------------------------------------------------
    /* Comparison process in Thread */
    void CompareTask()
    {
      try
      {
        cmp = DSComparer.Compare(
          new[] { profile.SrcA.DT, profile.SrcB.DT }, 
          profile.MatchInOrder ? MatchType.NoMatch : MatchType.Selected, 
          profile.MatchAllPairs ? MatchType.All : MatchType.Selected, 
          profile.CheckRepeats, 
          profile.TryConvert, 
          profile.NullAsStr, 
          profile.CaseSens,
          profile.DiffOnly, 
          profile.OnlyA, 
          profile.OnlyB,          
          new[] { profile.Cols.Select(x => x.ColA), profile.Cols.Select(x => x.ColB) },
          new[] { profile.Cols.Where(x => x.Key).Select(x => x.ColA), profile.Cols.Where(x => x.Key).Select(x => x.ColB) },
          new[] { profile.Cols.Where(x => x.Match).Select(x => x.ColA), profile.Cols.Where(x => x.Match).Select(x => x.ColB) },
          (x) => {
            if (contextCompare.ViewContext != null)
              contextCompare.ViewContext.Send(CompareProgress, x); // progress action in sync context
            else
              CompareProgress(x);
          });

        if (contextCompare.ViewContext != null)
          contextCompare.ViewContext.Send(CompareFinish, true); // finish action in sync context
        else
          CompareFinish(true);
      }
      catch (ThreadAbortException) // if thread aborted
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
    /* Comparison progress */
    void CompareProgress(object step) // step = Tuple<int = step number, string = current message>
    {
      if (contextCompare.OnProgress != null)
        contextCompare.OnProgress((step as Tuple<int, string>).Item1, "Compare: " + (step as Tuple<int, string>).Item2);
    }
    //-------------------------------------------------------------------------
    /* Stop all processes (get data or comparison) */
    void CompareStop()
    {
      contextCompare.Cancel = true;
      bool errA = (profile.SrcA.Content.Error != null),
        errB = (profile.SrcB.Content.Error != null);

      // Forced termination of get data process if no errors
      if (profile.SrcA.InProc && !errA)
        profile.SrcA.Content.GetDataStop();
      if (profile.SrcB.InProc && !errB)
        profile.SrcB.Content.GetDataStop();

      if (errA)
        contextCompare.OnError("Error in " + profile.SrcA.Name, profile.SrcA.Content.Error);
      if (errB)
        contextCompare.OnError("Error in " + profile.SrcB.Name, profile.SrcB.Content.Error);

      bool closeProgressView = !errA && !errB; // progress view will close only if no errors
      if (taskCompare != null && taskCompare.IsAlive) // comparison thread is alive
      {
        taskCompare.Abort(); // Let's simply beat it not long thinking...
        taskCompare.Join(0);
        ClearResult();
        CompareFinish(closeProgressView);
      }
      else if (contextCompare.OnFinish != null) // comparison thread is not alive and exist finish handler in context
        contextCompare.OnFinish(closeProgressView, null); 
    }
    //-------------------------------------------------------------------------
    /* Finish comparison process */
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
      if (!fail) CompareResult(); // show result if OK
      if (contextCompare.OnFinish != null) // exec finish handler from context
        contextCompare.OnFinish((bool)closeProgressView, null); 
    }
    //-------------------------------------------------------------------------
    /* Get comparison result */
    void CompareResult()
    {
      try
      {
        if (batch) // in batch-mode - to file
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
        else // in win mode - in form
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
    /* Send comparison result */
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
    /* Clear comparison result */
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
    /* Forming pairs of fields from field lists */
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
    /* Delete pairs with specified indices */
    void RemoveColPair(int[] idxs)
    {
      if (profile.Cols == null) return;
      foreach (ColPair r in idxs.Select(x => profile.Cols[x]).ToList())
        profile.Cols.Remove(r);
    }
    //-------------------------------------------------------------------------
    /* Moving pairs with specified indices */
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

      return idxs.Min() + offset; // where the current index moved
    }
    //-------------------------------------------------------------------------
    /* Lists of source field names, except for existing in pairs */
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
    //~~~~~~~~ Source command handlers (winmode) ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    /* Command handler from source A */
    object CommandSourceA(string command, object param)
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
    /* Command handler from source B */
    object CommandSourceB(string command, object param)
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
    /* Source command processing */
    object CommandSource(Source src, string command, object param)
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

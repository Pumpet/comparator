using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Windows.Forms;
using Common;

namespace SqlSource
{
  public enum ProviderType { MSSQL, Sybase, SybaseASE15, Oracle, OleDB, ODBC }
  public interface ISqlModel // интерфейс параметров коннекта и запроса
  {
    ProviderType Provider { get; set; }
    string Server { get; set; }
    string DB { get; set; }
    string Login { get; set; }
    string Pwd { get; set; }
    string ConnStr { get; set; }
    int CommandTimeout { get; set; }
    string SQL { get; set; }
    List<string> Fields { get; set; }
  }
  //===========================================================================
  class SqlModel : ISqlModel // стандартная реализация ISqlModel
  {
    public ProviderType Provider { get; set; }
    public string Server { get; set; }
    public string DB { get; set; }
    public string Login { get; set; }
    public string Pwd { get; set; }
    public string ConnStr { get; set; }
    public int CommandTimeout { get; set; }
    public string SQL { get; set; }
    public List<string> Fields { get; set; }
    public SqlModel()
    {
      Provider = ProviderType.MSSQL;
      CommandTimeout = 15;
      Fields = new List<string>();
    }
  }
  //===========================================================================
  public class SqlController
  {
    ISqlView view;
    ISqlModel model;
    DataTable dtResult;
    DbDataAdapter daResult;
    Thread task; // процесс получения данных
    TaskContext taskParam = new TaskContext(); // контекст выполнения процесса (задается при запуске процесса получения данных)

    public ISqlView View { get { return view; } }
    public Form Form { get { return view is Form ? (Form)view : new Form(); } }
    string NetProvider { get { return model.Provider == ProviderType.ODBC ? "System.Data.Odbc" : "System.Data.OleDb"; } }

    //~~~~~~~~ Init ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~    
    #region
    //-------------------------------------------------------------------------
    public static SqlController CreateSqlController(ISqlModel m = null)
    {
      SqlView f = new SqlView();
      if (m == null) m = new SqlModel();
      SqlController c = new SqlController(m, f);
      return c;
    }
    //-------------------------------------------------------------------------        
    public SqlController(ISqlModel m, ISqlView v)
    {
      view = v;
      model = m;
      view.TestConnect += TestConnect;
      view.GetData += GetData;
      view.StopGetData += StopGetData;

      Dictionary<string, string> pn = new Dictionary<string, string>();
      pn.Add("Provider", "Provider");
      pn.Add("Server", "Server");
      pn.Add("DB", "DB");
      pn.Add("Login", "Login");
      pn.Add("Pwd", "Pwd");
      pn.Add("ConnStr", "ConnStr");
      pn.Add("SQL", "SQL");
      pn.Add("CommandTimeout", "CommandTimeout");
      view.SetDataProps(pn);

      view.SetData(model, Enum.GetValues(typeof(ProviderType)), model.Fields);
    }
    #endregion
    //~~~~~~~~ Handlers ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    bool TestConnect(Action<string, Exception> onErr)
    {
      try
      {
        DbConnection conn = GetConnection();
        conn.Open();
        conn.Close();
        return true;
      }
      catch (Exception ex)
      {
        if (onErr != null)
          onErr("Connection failed!", ex);
        else
          throw;
        return false;
      }
    }
    //-------------------------------------------------------------------------
    /* запуск процесса получения данных */
    public void GetData(string sql, TaskContext context)
    {
      taskParam = context ?? new TaskContext();
      taskParam.Cancel = false;
      taskParam.Error = null;

      sql = (string.IsNullOrEmpty(sql.Trim()) ? model.SQL : sql).Trim();

      task = new Thread(GetDataTask);
      task.Name = "GetDataTask";
      task.IsBackground = true;
      task.Start(sql);
    }
    //-------------------------------------------------------------------------
    /* остановка процесса получения данных */
    public void StopGetData()
    {
      if (task != null && task.IsAlive)
      {
        taskParam.Cancel = true;
        task.Abort(); // приходится срубать так, иначе не достучишься
        task.Join(0); 
        OnTaskFinish(null);
      }
    }
    #endregion
    //~~~~~~~~ GetData Task ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region 
    //-------------------------------------------------------------------------
    /* внутри процесса получения данных */
    void GetDataTask(object sql)
    {
      DbConnection conn = null;
      try
      {
        conn = GetConnection();
        ClearMemory();
        dtResult = new DataTable();
        dtResult.RowChanged += ResultRowChange; // для возможности мониторить ход процесса начитки записей
        daResult = DbProviderFactories.GetFactory(NetProvider).CreateDataAdapter();
        daResult.SelectCommand = conn.CreateCommand();
        daResult.SelectCommand.CommandText = sql.ToString();
        daResult.SelectCommand.CommandTimeout = model.CommandTimeout >= 0 ? model.CommandTimeout : 0;
        if (string.IsNullOrEmpty(daResult.SelectCommand.CommandText))
          throw new Exception("No SQL command!");
        daResult.Fill(dtResult);
        if (taskParam.ViewContext != null)
          taskParam.ViewContext.Send(OnTaskFinish, null); // действия по окончании - выполняем в контексте внешнего вью
        else
          OnTaskFinish(null);
        
      }
      catch (ThreadAbortException) // срубили процесс
      {
        if (dtResult != null)
          dtResult.RowChanged -= ResultRowChange;
        if (conn != null)
          conn.Close();
      }
      catch (OutOfMemoryException ex)
      {
        ClearMemory();
        taskParam.Error = new Exception("Out of memory! Your query is VERY HUGE!", ex);
        if (taskParam.ViewContext != null)
          taskParam.ViewContext.Send(OnTaskFinish, null);
        else
          OnTaskFinish(null);
      }
      catch (Exception ex)
      {
        taskParam.Error = ex;
        if (taskParam.ViewContext != null)
          taskParam.ViewContext.Send(OnTaskFinish, null);
        else
          OnTaskFinish(null);
      }
      finally
      {
        if (dtResult != null)
          dtResult.RowChanged -= ResultRowChange;
        if (conn != null)
          conn.Close();
      }
    }
    //-------------------------------------------------------------------------
    public void ClearMemory()
    {
      if (dtResult != null)
      {
        //dtResult.Dispose();
        dtResult = null;
        GC.Collect();
      }
      if (daResult != null)
      {
        daResult.Dispose();
        daResult = null;
        GC.Collect();
      }
    }
    //-------------------------------------------------------------------------
    /* монитор начитки каждой строки */
    void ResultRowChange(object sender, DataRowChangeEventArgs e)
    {
      if (e.Action == DataRowAction.Commit && dtResult.Rows.Count % 100 == 0)
      {
        if (taskParam.ViewContext != null)
          taskParam.ViewContext.Send(OnTaskProgress, dtResult.Rows.Count); // ход процесса - выполняем в контексте внешнего вью
        else
          OnTaskProgress(dtResult.Rows.Count);
      }
    }
    //-------------------------------------------------------------------------
    /* ход процесса получения данных */
    void OnTaskProgress(object step)
    {
      if (taskParam.OnProgress != null && !taskParam.Cancel)
        taskParam.OnProgress((int)step, string.Format("{0} rows received...", dtResult.Rows.Count));
    }
    //-------------------------------------------------------------------------
    /* завершение получения данных */
    void OnTaskFinish(object state) // state нужен только потому, что нужен параметр при вызове через SynchronizationContext
    {
      string msg = "";
      model.Fields.Clear();
      if (dtResult != null)
        model.Fields.AddRange(dtResult.Columns.OfType<DataColumn>().Select(x => x.ColumnName).ToList());

      if (taskParam.Cancel)
        msg = "Stopped";
      if (taskParam.Error != null)
      {
        msg = "Stopped on error";
        if (taskParam.OnError != null)
          taskParam.OnError("Error get data:", taskParam.Error);
        else
          throw taskParam.Error;
      }

      if (taskParam.OnFinish != null)
        taskParam.OnFinish(dtResult, string.Format("{0} rows {1}", dtResult != null ? dtResult.Rows.Count : 0, msg));
    }
    #endregion
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    DbConnection GetConnection()
    {
      DbConnectionStringBuilder b = new DbConnectionStringBuilder();
      switch (model.Provider)
      {
        case ProviderType.MSSQL:
          b["Provider"] = "SQLOLEDB";
          b["Data Source"] = model.Server;
          b["Initial Catalog"] = model.DB;
          b["Connection Timeout"] = "5";
          if (string.IsNullOrEmpty(model.Login))
            b["Integrated Security"] = "SSPI";
          else
          {
            b["User id"] = model.Login;
            b["Password"] = model.Pwd;
          }
          break;
        case ProviderType.SybaseASE15: // используем sybdrvoledb.dll версии 15.0.0.325, более поздние давали кракозябры, если на сервере iso_1
          b["Provider"] = "ASEOLEDB";
          b["Data Source"] = model.Server;
          b["Initial Catalog"] = model.DB;
          b["User id"] = model.Login;
          b["Password"] = model.Pwd;
          b["Language"] = "us_english"; // нужно проверять, везде ли это прокатит, однако если не ставить, можем получить: "Language name in login record 'russian' is not an official name on this ASE. Using default 'us_english' from syslogins instead."
          break;
        case ProviderType.Sybase:
          b["Provider"] = "Sybase.ASEOLEDBProvider";
          b["Server Name"] = model.Server;
          b["Initial Catalog"] = model.DB;
          b["User id"] = model.Login;
          b["Password"] = model.Pwd;
          break;
        case ProviderType.Oracle:
          b["Provider"] = "msdaora";
          b["Data Source"] = model.Server;
          b["User id"] = model.Login;
          b["Password"] = model.Pwd;
          break;
        case ProviderType.OleDB:
          b.ConnectionString = model.ConnStr;
          break;
        case ProviderType.ODBC:
          b.ConnectionString = model.ConnStr;
          break;
        default:
          break;
      }
      DbConnection conn = DbProviderFactories.GetFactory(NetProvider).CreateConnection();
      conn.ConnectionString = b.ConnectionString;
      return conn;
    }
    #endregion
  }
}



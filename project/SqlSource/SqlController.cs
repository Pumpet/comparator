//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  License: GNU Lesser General Public License (LGPLv3)
//
//  Email: pumpet.net@gmail.com
//  Git: https://github.com/Pumpet/comparator
//  Copyright (C) Alex Rozanov, 2016 
//

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
  //==========================================================================
  /* Connect and query parameters */
  public interface ISqlModel 
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
  /* Default connect and query parameters */
  class SqlModel : ISqlModel
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
  /* SQL connect and query controller */
  public class SqlController
  {
    ISqlView view; 
    ISqlModel model;
    DataTable dtResult;
    DbDataAdapter daResult;
    Thread task; // getting data process
    TaskContext taskParam = new TaskContext(); // getting data process context (set on start getting data process)

    public ISqlView View { get { return view; } }
    public Form Form { get { return view is Form ? (Form)view : new Form(); } }
    string NetProvider { get { return model.Provider == ProviderType.ODBC ? "System.Data.Odbc" : "System.Data.OleDb"; } }

    //~~~~~~~~ Init ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~    
    #region
    //-------------------------------------------------------------------------
    /* Create with standart Form */
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

      // view property : model property
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

      // set model data to view
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
    /* Start getting data process */
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
    /* Stop getting data process */
    public void StopGetData()
    {
      if (task != null && task.IsAlive)
      {
        taskParam.Cancel = true;
        task.Abort(); // stop daResult.Fill() - Who knows another way?
        task.Join(0); 
        OnTaskFinish(null);
      }
    }
    #endregion
    //~~~~~~~~ GetData Task ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region 
    //-------------------------------------------------------------------------
    /* Getting data process in Thread */
    void GetDataTask(object sql)
    {
      DbConnection conn = null;
      try
      {
        conn = GetConnection();
        ClearMemory();
        dtResult = new DataTable();
        dtResult.RowChanged += ResultRowChange; // handler getting row
        daResult = DbProviderFactories.GetFactory(NetProvider).CreateDataAdapter();
        daResult.SelectCommand = conn.CreateCommand();
        daResult.SelectCommand.CommandText = sql.ToString();
        daResult.SelectCommand.CommandTimeout = model.CommandTimeout >= 0 ? model.CommandTimeout : 0;
        if (string.IsNullOrEmpty(daResult.SelectCommand.CommandText))
          throw new Exception("No SQL command!");
        daResult.Fill(dtResult);
        if (taskParam.ViewContext != null)
          taskParam.ViewContext.Send(OnTaskFinish, null); // finish action in sync context
        else
          OnTaskFinish(null);
        
      }
      catch (ThreadAbortException) // if thread aborted
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
    /* Handler getting row */
    void ResultRowChange(object sender, DataRowChangeEventArgs e)
    {
      if (e.Action == DataRowAction.Commit && dtResult.Rows.Count % 100 == 0)
      {
        if (taskParam.ViewContext != null)
          taskParam.ViewContext.Send(OnTaskProgress, dtResult.Rows.Count); // progress action in sync context
        else
          OnTaskProgress(dtResult.Rows.Count);
      }
    }
    //-------------------------------------------------------------------------
    /* Progress of getting data process */
    void OnTaskProgress(object step)
    {
      if (taskParam.OnProgress != null && !taskParam.Cancel)
        taskParam.OnProgress((int)step, string.Format("{0} rows received...", dtResult.Rows.Count));
    }
    //-------------------------------------------------------------------------
    /* Finish of getting data process */
    void OnTaskFinish(object state) 
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
        case ProviderType.SybaseASE15: // test with sybdrvoledb.dll v.15.0.0.325 is correct with russian symbols in result
          b["Provider"] = "ASEOLEDB";
          b["Data Source"] = model.Server;
          b["Initial Catalog"] = model.DB;
          b["User id"] = model.Login;
          b["Password"] = model.Pwd;
          b["Language"] = "us_english"; 
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



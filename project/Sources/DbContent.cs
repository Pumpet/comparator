using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SqlSource;
using Common;

namespace Sources
{
  public class DbContent : SourceContent, ISqlModel // содержимое источника БД  
  {
    EncrDecr ed;
    SqlController dbController;
    ProviderType provider;
    //-- ISqlModel Members ---------------------------------------------------
    public ProviderType Provider
    {
      get { return provider; }
      set { provider = value; }
    }
    public string Server { get; set; }
    public string DB { get; set; }
    public string Login { get; set; }
    [XmlIgnore]
    public string Pwd { get; set; }
    [XmlIgnore]
    public string ConnStr { get; set; }
    public int CommandTimeout { get; set; }
    public string SQL { get; set; }
    /* ISqlModel.Fields реализовано в SourceContent */
    //-------------------------------------------------------------------------
    public string PwdEncr { get { return ed.Encrypt(Pwd); } set { Pwd = ed.Decrypt(value); } } // для сохранения
    public string ConnStrEncr { get { return ed.Encrypt(ConnStr); } set { ConnStr = ed.Decrypt(value); } } // для сохранения
    [XmlIgnore]
    public string ConnectionInfo // краткая инфа для вью
    {
      get
      {
        string info, nl = Environment.NewLine;
        if (Provider == ProviderType.OleDB || Provider == ProviderType.ODBC)
          info = string.Format(@"Source: {0}Connection string: {1}", Provider + nl, ConnStr + nl);
        else
          info = string.Format(@"Source: {0}Server: {1}DB: {2}Login: {3}",
            Provider + nl, Server + nl, DB + nl, Login + nl);
        return info;
      }
    }
    //-------------------------------------------------------------------------
    public DbContent()
    {
      Provider = ProviderType.OleDB;
      CommandTimeout = 15;
      Fields = new List<string>();
      ed = new EncrDecr(string.Empty, string.Empty);
      dbController = SqlController.CreateSqlController(this);
    }
    //-------------------------------------------------------------------------
    public ISqlView GetSqlView()
    {
      return dbController.View;
    }
    //-------------------------------------------------------------------------
    public override void Check()
    {
      switch (Provider)
      {
        case ProviderType.MSSQL:
        case ProviderType.Oracle:
        case ProviderType.Sybase:
        case ProviderType.SybaseASE15:
          if (string.IsNullOrEmpty(Server))
            throw new Exception(Parent.Name + ": server is not specified");
          break;
        case ProviderType.ODBC:
        case ProviderType.OleDB:
          if (string.IsNullOrEmpty(ConnStr))
            throw new Exception(Parent.Name + ": connection string is not specified");
          break;
        default:
          break;
      }
      if (string.IsNullOrEmpty(SQL))
        throw new Exception(Parent.Name + ": query is not specified");
    }
    //-------------------------------------------------------------------------
    public override List<string> GetCheckFields()
    {
      if (Fields.Count == 0) // поля источника БД появляются только после запуска запроса из ISqlView или из сверки
        throw new Exception(Parent.Name + ": no fields found. Run query to get!");
      return Fields;
    }
    //-------------------------------------------------------------------------
    public override void GetData(TaskContext c, Action<bool> a)
    {
      base.GetData(c, a);
      if (c != null && c.OnProgress != null)
        c.OnProgress(0, "execute...");
      dbController.GetData(SQL, context); // запуск процесса начитки данных в SqlController-е, обработчики хода, окончания и ошибки заданы в context
      // заполнение SourceContent.DT происходит в базовом GetDataEnd(), который вызывается при успешном окончании запущенного процесса
    }
    //-------------------------------------------------------------------------
    public override void GetDataStop()
    {
      dbController.StopGetData();
    }
  }
}

using System;
using Common;

namespace Comparator
{
  class Loger : ILoger
  {
    //~~~~~~~~ ILoger Members ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    #region
    //-------------------------------------------------------------------------
    public void Error(string mess, object objErr = null)
    {
      Log.Write(mess, objErr, true);
      //Console.WriteLine("-----------"); //test
      //Console.WriteLine("Press any key to exit..."); //test
      //Console.ReadKey(true); //test
      Environment.Exit(1);
    }
    //-------------------------------------------------------------------------
    public void Message(string mess, bool critical = false)
    {
      Log.Write((critical ? "WARNING: " : "") + mess, true);
    }
    #endregion
    //-------------------------------------------------------------------------
    public Loger(string file)
    {
      Log.Start(false, null, file);
    }
  }
}

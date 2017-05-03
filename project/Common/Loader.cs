using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Common
{
  /* сериализация в xml */
  public static class Loader
  {
    public static T Load<T>(string file) where T : class
    {
      T obj;
      if (!File.Exists(file))
        throw new Exception(String.Format("File not found {0}", file));
      try
      {
        XmlSerializer xs = new XmlSerializer(typeof(T));
        using (var s = new StreamReader(file, Encoding.GetEncoding("windows-1251")))
        {
          obj = (T)xs.Deserialize(s);
        }
      }
      catch (Exception ex)
      {
        throw new Exception(String.Format("Can't load object from file {0}", file), ex);
      }
      return obj;
    }
    //-------------------------------------------------------------------------
    public static void Save<T>(string file, T obj) where T : class
    {
      if (obj == null)
        return;
      try
      {
        XmlSerializer xs = new XmlSerializer(typeof(T));
        using (var s = new StreamWriter(file, false, Encoding.GetEncoding("windows-1251")))
        {
          xs.Serialize(s, obj);
        }
      }
      catch (Exception ex)
      {
        throw new Exception(String.Format("Can't save object to file {0}", file), ex);
      }
    }
  }
}

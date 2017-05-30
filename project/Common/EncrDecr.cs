using System;
using System.IO;
using System.Security.Cryptography;

namespace Common
{
  /* Encrypt-decrypt string */
  public class EncrDecr
  {
    AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
    byte[] aesKey = new byte[32]; 
    byte[] aesIV = new byte[16];  
    //-------------------------------------------------------------------------
    public EncrDecr(string strKey, string strIV)
    {
      for (int i = 0; i < (strKey ?? string.Empty).Length; i++)
        if (i < aesKey.Length) aesKey[i] = Convert.ToByte(strKey[i]);
      for (int i = 0; i < (strIV ?? string.Empty).Length; i++)
        if (i < aesIV.Length) aesIV[i] = Convert.ToByte(strIV[i]);
    }
    //-------------------------------------------------------------------------
    public string Encrypt(string str)
    {
      byte[] arrEncr;
      using (AesCryptoServiceProvider aesEncr = new AesCryptoServiceProvider())
      {
        ICryptoTransform encryptor = aesEncr.CreateEncryptor(aesKey, aesIV);
        using (MemoryStream msEncr = new MemoryStream())
        {
          using (CryptoStream csEncr = new CryptoStream(msEncr, encryptor, CryptoStreamMode.Write))
          {
            using (StreamWriter swEncr = new StreamWriter(csEncr))
            {
              swEncr.Write(str);
            }
            arrEncr = msEncr.ToArray();
          }
        }
      }
      return Convert.ToBase64String(arrEncr);
    }
    //-------------------------------------------------------------------------
    public string Decrypt(string strEncr)
    {
      if (string.IsNullOrEmpty(strEncr)) return strEncr;
      byte[] arrEncr = Convert.FromBase64CharArray(strEncr.ToCharArray(), 0, strEncr.ToCharArray().Length);
      string str;
      using (AesCryptoServiceProvider aesDecr = new AesCryptoServiceProvider())
      {
        ICryptoTransform decryptor = aesDecr.CreateDecryptor(aesKey, aesIV);
        using (MemoryStream msDecr = new MemoryStream(arrEncr))
        {
          using (CryptoStream csDecr = new CryptoStream(msDecr, decryptor, CryptoStreamMode.Read))
          {
            using (StreamReader srDecr = new StreamReader(csDecr))
            {
              str = srDecr.ReadToEnd();
            }
          }
        }
      }
      return str;
    }
  }
}

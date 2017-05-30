using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml.Serialization;

namespace Common
{
  public class MailParams
  {
    [XmlIgnore]
    public EncrDecr ed;
    public string Server = "";
    public string Port = "";
    public bool Ssl = false;
    public string User = "";
    public string Pwd = "";
    public string PwdEncr = ""; 
    public string From = "";
    public string FromAlias = "";
    public MailParams()
    {
      ed = new EncrDecr(string.Empty, string.Empty);
    }
  }
  //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  
  public static class Mailer
  {
    public static void SendMail(MailParams par, string to, string subject, string body, string[] attachFiles)
    {
      MailMessage mail = new MailMessage();
      try
      {
        mail.From = String.IsNullOrEmpty(par.FromAlias) ? new MailAddress(par.From) : new MailAddress(par.From, par.FromAlias);
        mail.To.Add(to.Replace(';', ',').TrimEnd(new[]{','}));
        mail.Subject = subject;
        mail.IsBodyHtml = true;
        mail.Body = body;
        if (attachFiles != null)
          for (int i = 0; i < attachFiles.Length; i++)
          {
            string fn = Path.GetFileName(attachFiles[i]);
            System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType();
            
            ct.CharSet = null;
            ct.Boundary = null;
            ct.MediaType = "application/octet-stream";
            ct.Name = fn ?? String.Empty;
            if (ct.Name != fn) // problem with russian names is more than 20 characters - they turn into Base64. Don't know to solve...
              ct.Name = fn.Substring(0, 10) + "~" + Path.GetExtension(fn);
            Attachment att = new Attachment(attachFiles[i], ct);
            mail.Attachments.Add(att);
          }

        SmtpClient client = new SmtpClient(par.Server, string.IsNullOrEmpty(par.Port) ? 25 : int.Parse(par.Port));
        client.EnableSsl = par.Ssl;
        if (!string.IsNullOrEmpty(par.User))
        {
          string pwd = string.IsNullOrEmpty(par.PwdEncr) ? "" : par.ed.Decrypt(par.PwdEncr);
          client.Credentials = new NetworkCredential(par.User, pwd);
        }
        client.DeliveryMethod = SmtpDeliveryMethod.Network;

        client.Send(mail);
      }
      finally
      {
        mail.Dispose();
      }
    }

  }
}

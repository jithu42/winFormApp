using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HamburgerMenuApp.Core
{
    public static class emailClass
    {
        static public string Username
        {
            get { return ConfigurationManager.AppSettings["EmailUsername"]; }
        }

        static public string Password
        {
            get { return ConfigurationManager.AppSettings["EmailPassword"]; }
        }
        public static SmtpClient client = new SmtpClient();
        public static MailMessage msg = new MailMessage();
        public static System.Net.NetworkCredential smptCrede = new System.Net.NetworkCredential(Username, Password);
        public static bool SendEmail(string sendTo, string sendFrom, string subject, string body)
        {
            try
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.UseDefaultCredentials = false;
                client.Credentials = smptCrede;
                client.EnableSsl = true;
                MailAddress to = new MailAddress(sendTo);
                MailAddress from = new MailAddress(sendFrom);
                msg.Subject = subject;
                msg.Body = body;
                msg.From = from;
                msg.To.Add(to);
                client.Send(msg);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}

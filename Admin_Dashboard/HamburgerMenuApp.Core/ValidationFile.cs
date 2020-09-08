using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HamburgerMenuApp.Core
{
    public class ValidationFile
    {
        public static bool IsAlphaNumeric(string val)
        {
            Regex r = new Regex("^[a-zA-Z0-9_]+$");
            if (r.IsMatch(val))
            {
                return true;
            }
            return false;
        }

        public static bool IsNumeric(string val)
        {
            Regex r = new Regex("^[0-9]+$");
            if (r.IsMatch(val))
            {
                if (val.Length == 10)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsAlpha(string val)
        {
            Regex r = new Regex("^[a-zA-Z]+$");
            if (r.IsMatch(val))
            {
                return true;
            }
            return false;
        }

        public static string _getusername { get; set; }
    }
}

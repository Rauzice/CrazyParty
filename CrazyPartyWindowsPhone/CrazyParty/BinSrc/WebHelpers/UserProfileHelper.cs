using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace CrazyParty.BinSrc
{
    public class UserProfileHelper
    {
        public bool CheckEmailFormat(string email)
        {
            Regex reg = new Regex(@"^[a-z]([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\.][a-z]{2,3}([\.][a-z]{2})?$");
            return reg.IsMatch(email);
        }

        public bool CheckPasswordFormat(string password)
        {
            if (password.Length < 6 || password.Length > 20)
                return false;
            return true;
        }

        public bool CheckEmailUnused(string email)
        {
            return false;
        }
    }
}

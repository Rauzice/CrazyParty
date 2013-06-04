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
using CrazyParty.BinSrc.Model;
using System.Runtime.Serialization;

namespace CrazyParty.BinSrc
{
    public class LoginResult
    {
        public LoginResult()
        {
            code = 0;
            message = "";
            user = new UserInfo();
        }

        [DataMember]
        public int code { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public UserInfo user { get; set; }
    }

    public class RegisterResult
    {
        public RegisterResult()
        {
            code = 0;
            message = "";
        }
        public int code { get; set; }
        public string message { get; set; }
    }
}

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
    [DataContract]
    public class LoginResponce : BinResponce
    {
        public LoginResponce()
        {
            code = 0;
            content = new ResLoginContent();
        }


        [DataMember]
        public ResLoginContent content { get; set; }
    }

    [DataContract]
    public class RegisterResponce : BinResponce
    {
        public RegisterResponce()
        {
            code = 0;
            content = new ResRegisterContent();
        }

        [DataMember]
        public ResRegisterContent content { get; set; }
    }
}

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
using System.Runtime.Serialization;

namespace CrazyParty.BinSrc.Model
{
    [DataContract]
    public class ResLoginContent
    {
        public ResLoginContent()
        {
            userid = 0;
            user = new UserInfo();
            message = "";
        }

        [DataMember]
        public uint userid { get; set; }

        [DataMember]
        public UserInfo user { get; set; }

        [DataMember]
        public string message { get; set; }
    }
}

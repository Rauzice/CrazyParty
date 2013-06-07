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
    public class ResShowResultContent
    {
        public ResShowResultContent()
        {
            dices = "00000";
            win = false;
            message = "";
        }

        [DataMember]
        public string dices { get; set; }

        [DataMember]
        public bool win { get; set; }


        [DataMember]
        public string message { get; set; }
    }
}

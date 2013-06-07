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
    public class ResLiarDiceGameStateContent
    {
        public ResLiarDiceGameStateContent()
        {
            liarCount = 0;
            liarPoint = 0;
        }

        [DataMember]
        public int liarCount { get; set; }

        [DataMember]
        public int liarPoint { get; set; }

        [DataMember]
        public int message { get; set; }
    }
}

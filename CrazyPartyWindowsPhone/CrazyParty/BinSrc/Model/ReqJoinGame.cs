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
    public class ReqJoinGame : BinRequest
    {
        public ReqJoinGame()
        {
            code = GlobalVars.RequestCode.JoinCode;
            content = new ReqJoinGameContent();
        }

        public ReqJoinGame(ReqJoinGameContent p_content)
        {
            code = GlobalVars.RequestCode.JoinCode;
            content = p_content;
        }

        [DataMember]
        public ReqJoinGameContent content { get; set; }
    }
}

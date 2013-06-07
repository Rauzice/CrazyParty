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
    public class ReqCancelJoinGame : BinRequest
    {
        public ReqCancelJoinGame()
        {
            code = GlobalVars.RequestCode.CancelJoinCode;
            content = new ReqJoinGameContent();
        }

        public ReqCancelJoinGame(ReqJoinGameContent p_content)
        {
            code = GlobalVars.RequestCode.CancelJoinCode;
            content = p_content;
        }

        [DataMember]
        public ReqJoinGameContent content { get; set; }
    }
}

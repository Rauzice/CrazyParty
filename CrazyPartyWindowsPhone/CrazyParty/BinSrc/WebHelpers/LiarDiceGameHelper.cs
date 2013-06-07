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
using System.Runtime.Serialization.Json;
using System.IO;
using WebSocket4Net;
using System.Collections.Generic;

namespace CrazyParty.BinSrc
{
    public class LiarDiceGameHelper : GameHelper
    {
        public LiarDiceGameHelper(MainPage mp)
            : base(mp)
        {
        }

        protected override void websocket_Opened(object sender, EventArgs e)
        {
            base.websocket_Opened(sender, e);

            ReqJoinGameContent content = new ReqJoinGameContent() { gamename = GlobalVars.LiarDiceGameName, userid=GlobalVars.userid};
            ReqJoinGame joinGameRequest = new ReqJoinGame(content);

            List<Type> knowTypes = new List<Type>();
            knowTypes.Add(typeof(ReqJoinGameContent));

            string msg = JsonHelper.ObjectToJson(typeof(ReqJoinGame), knowTypes, joinGameRequest);
            websocket.Send(msg);

            //string jsonStr = @"{code:100, content:{userid:435234, gameName:'liardice'}}";
            //ReqJoinGame sdf = JsonHelper.JsonToObject(typeof(ReqJoinGame), knowTypes, jsonStr) as ReqJoinGame;
            //ReqJoinGameContent sd = sdf.content;
        }

        protected override void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            base.websocket_MessageReceived(sender, e);
            try
            {
                BinResponce res = JsonHelper.JsonToObject(typeof(BinResponce), null, e.Message) as BinResponce;

                switch (res.code)
                {
                    case 100:
                        {
                            mainPage.Dispatcher.BeginInvoke(new Action(() => {
                                mainPage.GotoOnlinePage();
                            }));
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ;
            }
        }

        #region public functions

        public void sendState()
        {
        }

        public override void Disconnect()
        {
            ReqJoinGameContent content = new ReqJoinGameContent() { gamename = GlobalVars.LiarDiceGameName, userid = GlobalVars.userid };
            ReqCancelJoinGame joinGameRequest = new ReqCancelJoinGame(content);

            List<Type> knowTypes = new List<Type>();
            knowTypes.Add(typeof(ReqJoinGameContent));

            string msg = JsonHelper.ObjectToJson(typeof(ReqCancelJoinGame), knowTypes, joinGameRequest);

            try
            {
                websocket.Send(msg);
                base.Disconnect();
            }
            catch (Exception)
            {
                MessageBox.Show("网络异常.");
                return;
            }
        }

        #endregion
    }
}

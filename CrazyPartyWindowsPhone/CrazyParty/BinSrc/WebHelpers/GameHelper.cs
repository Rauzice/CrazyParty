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
using WebSocket4Net;
using SuperSocket.ClientEngine;
using System.Text;

namespace CrazyParty.BinSrc
{
    public class GameHelper
    {
        public GameHelper(Page p)
        {
            page = p;

            websocket = new WebSocket(GlobalVars.GameWSServer);
            websocket.Opened += new EventHandler(websocket_Opened);
            websocket.Error += new EventHandler<ErrorEventArgs>(websocket_Error);
            websocket.Closed += new EventHandler(websocket_Closed);
            websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);
        }

        WebSocket websocket;
        Page page;

        private void websocket_Opened(object sender, EventArgs e)
        {
                websocket.Send("Hello World!");
        }

        private void websocket_Error(object sender, ErrorEventArgs e)
        {
        }

        private void websocket_Closed(object sender, EventArgs e)
        {
            
        }

        private void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            //MessageBox.Show(e.Message);
            page.Dispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(e.Message);
            }
            ));
        }

        public void connect()
        {
            if (websocket.State == WebSocketState.None || websocket.State == WebSocketState.Closed)
                websocket.Open();
            else
                websocket.Send("websocket has opened.");
        }
    }
}

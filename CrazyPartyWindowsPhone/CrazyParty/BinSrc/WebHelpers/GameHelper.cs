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
using System.Windows.Threading;

namespace CrazyParty.BinSrc
{
    public class GameHelper
    {
        public GameHelper(MainPage p)
        {
            mainPage = p;

            websocket = new WebSocket(GlobalVars.GameWSServer);
            websocket.Opened += new EventHandler(websocket_Opened);
            websocket.Error += new EventHandler<ErrorEventArgs>(websocket_Error);
            websocket.Closed += new EventHandler(websocket_Closed);
            websocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(websocket_MessageReceived);

            connectTimer = new DispatcherTimer();
            connectTimer.Interval = TimeSpan.FromSeconds(6);
            connectTimer.Tick += new EventHandler(connectTimer_Tick);
            stopTimer = new StopTimer(connectTimer.Stop);
        }

        protected WebSocket websocket;
        protected MainPage mainPage;

        protected Page gamePage;

        public Page GamePage
        {
            set { gamePage = value; }
        }

        protected virtual void websocket_Opened(object sender, EventArgs e)
        {
            mainPage.Dispatcher.BeginInvoke(new Action(()=>
            {
                connectTimer.Stop();
                mainPage.GotoOnlinePage();
            }));
            
        }

        protected void websocket_Error(object sender, ErrorEventArgs e)
        {
        }

        protected void websocket_Closed(object sender, EventArgs e)
        {
            
        }

        protected virtual void websocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
        }

        protected DispatcherTimer connectTimer = new DispatcherTimer();

        protected void connectTimer_Tick(object o, EventArgs e)
        {
            if (websocket.State == WebSocketState.Open)
                return;
            else
            {
                mainPage.Dispatcher.BeginInvoke(new Action(() =>
                {
                    MessageBox.Show("连接失败，请检查您的网络。");
                }
                ));
                try
                {
                    websocket.Close();
                }
                catch (Exception)
                {
                    ;
                }
                connectTimer.Stop();
            }
        }

        public delegate void StopTimer();
        StopTimer stopTimer;

        public void connect()
        {
            if (websocket.State == WebSocketState.None || websocket.State == WebSocketState.Closed)
            {
                websocket.Open();
                connectTimer.Start();
            }
            else if (websocket.State == WebSocketState.Closing)
            {
                while (websocket.State == WebSocketState.Closing)
                    ;
                websocket.Open();
                connectTimer.Start();
            }
            else if (websocket.State == WebSocketState.Connecting)
            {
                connectTimer.Start();
            }
        }

        public void refreshOnlineGameInfo()
        {

        }

        public void sendUserInfo()
        {
        }

        public void sendPrepared()
        {
        }

        public virtual void Disconnect()
        {
            if(websocket!=null && websocket.State != WebSocketState.None)
                websocket.Close();
        }

        public void sendExitInfo()
        {
        }
    }
}

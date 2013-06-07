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
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using CrazyParty.BinSrc.Model;

namespace CrazyParty.BinSrc
{
    public class GameInfoHelper
    {
        public GameInfoHelper(MainPage p)
        {
            mainPage = p;
        }

        public void LoginInit()
        {
            loginReturned = false;
            loginTimer = new DispatcherTimer();
            lr = new LoginResponce();
            loginTimer.Interval = TimeSpan.FromSeconds(10);
        }

        private MainPage mainPage;

        private bool loginReturned;
        private DispatcherTimer loginTimer;
        private LoginResponce lr;

        public void GetGameInfo()
        {
            LoginInit();

            GlobalVars.userid = 0;


            Uri loginPath = new Uri(GlobalVars.WebServer + "/gameinfo/liardice");

            WebClient loginWC = new WebClient();
            loginWC.Encoding = Encoding.UTF8;

            loginWC.DownloadStringCompleted += (sender, e) =>
            {
                if (loginReturned)
                {
                    return;
                }
                loginReturned = true;
                if (e.Error != null)
                {
                    lr = new LoginResponce() { code = 404, content = new  ResLoginContent() { message = e.Error.Message } };
                }
                else
                {
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(e.Result));
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LoginResponce));
                    lr = (LoginResponce)serializer.ReadObject(ms);
                }
                if (lr.code == 200)
                {
                    loginSuccess(lr.content as ResLoginContent);
                }
                else
                {
                    loginFailed(lr.content as ResLoginContent);
                }
            };
            loginWC.DownloadStringAsync(loginPath);

            loginTimer.Tick += (sen, er) =>
            {
                if (loginReturned)
                    return;
                loginTimer.Stop();
                loginReturned = true;
                lr = new LoginResponce() { code = 400, content = new ResLoginContent() { message = "连接服务器超时." } };
                loginFailed(lr.content as ResLoginContent);
            };
            loginTimer.Start();
        }

        private void loginSuccess(ResLoginContent content)
        {
            GlobalVars.userid = content.userid;

            mainPage.Dispatcher.BeginInvoke(new Action(() =>
            {
                mainPage.GotoOnlinePage();
            }));
        }

        private void loginFailed(ResLoginContent content)
        {
            mainPage.Dispatcher.BeginInvoke(new Action(() =>
            {
                string msg = "登陆失败. " + content.message;
                MessageBox.Show(msg);
                mainPage.loginSubmitBtn.IsEnabled = true;
            }));
        }
    }
}

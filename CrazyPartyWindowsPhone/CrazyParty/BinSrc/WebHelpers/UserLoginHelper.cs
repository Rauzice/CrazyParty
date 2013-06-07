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
    public class UserLoginHelper
    {
        public UserLoginHelper(MainPage p)
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

        public bool CheckUserNameFormat(string username)
        {
            if (username.Length < 4 || username.Length > 40)
                return false;
            Regex reg = new Regex(@"^\w+$");
            return reg.IsMatch(username);
        }

        public bool CheckPasswordFormat(string password)
        {
            if (password.Length < 6 || password.Length > 20)
                return false;
            return true;
        }

        private bool loginReturned;
        private DispatcherTimer loginTimer;
        private LoginResponce lr;

        public void Login(string username, string password, MainPage mp)
        {
            LoginInit();

            GlobalVars.userid = 0;

            if (!CheckUserNameFormat(username))
            {
                lr.code = 101;
                lr.content.message = "用户名格式不正确";
                loginFailed(lr.content as ResLoginContent);
                return;
            }
            if (!CheckPasswordFormat(password))
            {
                lr.code = 102;
                lr.content.message = "密码长度应为6—20";
                loginFailed(lr.content as ResLoginContent);
                return;
            }

            password = GlobalVars.userRegisterHelper.GetMd5Hash(password);

            StringBuilder parameters = new StringBuilder();
            parameters.AppendFormat("{0}={1}", "username", HttpUtility.UrlEncode(username));
            parameters.AppendFormat("&{0}={1}", "password", HttpUtility.UrlEncode(password));

            Uri loginPath = new Uri(GlobalVars.WebServer + "/login");

            WebClient loginWC = new WebClient();
            loginWC.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            loginWC.Encoding = Encoding.UTF8;

            loginWC.UploadStringCompleted += (sender, e) =>
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
            loginWC.UploadStringAsync(loginPath, "POST", parameters.ToString());

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
                mainPage.usernameTextBlock.Text = mainPage.loginName.Text;
                mainPage.loginGrid.Visibility = System.Windows.Visibility.Collapsed;
                mainPage.userGrid.Visibility = System.Windows.Visibility.Visible;
                mainPage.loginSubmitBtn.IsEnabled = true;
                mainPage.startOnlineGameBtn.IsEnabled = true;
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

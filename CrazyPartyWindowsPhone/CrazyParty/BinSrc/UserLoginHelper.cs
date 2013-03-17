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
        public void Init()
        {
            loginReturned = false;
            loginTimer = new DispatcherTimer();
            lr = new LoginResult();
            loginTimer.Interval = TimeSpan.FromSeconds(10);
        }

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
        private LoginResult lr;

        public void Login(string username, string password, MainPage mp)
        {
            if (!CheckUserNameFormat(username))
            {
                lr.code = 101;
                lr.message = "用户名格式不正确";
                loginFailed(mp);
                return;
            }
            if (!CheckPasswordFormat(password))
            {
                lr.code = 102;
                lr.message = "密码长度应为6—20";
                loginFailed(mp);
                return;
            }

            password = GlobalVars.userRegisterHelper.GetMd5Hash(password);

            StringBuilder parameters = new StringBuilder();
            parameters.AppendFormat("{0}={1}", "username", HttpUtility.UrlEncode(username));
            parameters.AppendFormat("&{0}={1}", "password", HttpUtility.UrlEncode(password));

            Uri loginPath = new Uri(GlobalVars.Server + "/login");

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
                    lr = new LoginResult() { code = 404, message = e.Error.Message };
                }
                else
                {
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(e.Result));
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LoginResult));
                    lr = (LoginResult)serializer.ReadObject(ms);
                }
                if (lr.code == 200)
                {
                    loginSuccess(mp);
                }
                else
                {
                    loginFailed(mp);
                }
            };
            loginWC.UploadStringAsync(loginPath, "POST", parameters.ToString());

            loginTimer.Tick += (sen, er) =>
            {
                if (loginReturned)
                    return;
                loginTimer.Stop();
                loginReturned = true;
                lr = new LoginResult() { code = 400, message = "连接服务器超时." };
                loginFailed(mp);
            };
            loginTimer.Start();
        }

        private void loginSuccess(MainPage mp)
        {
            mp.usernameTextBlock.Text = mp.loginName.Text;
            mp.loginGrid.Visibility = System.Windows.Visibility.Collapsed;
            mp.userGrid.Visibility = System.Windows.Visibility.Visible;
            ((Microsoft.Phone.Shell.ApplicationBarIconButton)mp.ApplicationBar.Buttons[0]).IsEnabled = true;
        }

        private void loginFailed(MainPage mp)
        {
            string msg = "登陆失败. " + lr.message;
            MessageBox.Show(msg);
            ((Microsoft.Phone.Shell.ApplicationBarIconButton)mp.ApplicationBar.Buttons[0]).IsEnabled = true;
        }
    }
}

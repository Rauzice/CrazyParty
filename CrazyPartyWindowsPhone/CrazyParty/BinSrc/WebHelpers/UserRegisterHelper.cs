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
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Windows.Threading;
using CrazyParty.BinSrc.Pages;

namespace CrazyParty.BinSrc
{
    public class UserRegisterHelper
    {
        public void Init(RegisterPage rp)
        {
            _registerPage = rp;
            registerReturned = false;
            registerTimer = new DispatcherTimer();
            registerTimer.Interval = TimeSpan.FromSeconds(10);
            rr = new RegisterResponce();
        }

        private RegisterPage _registerPage;

        private DispatcherTimer registerTimer;
        private bool registerReturned;
        private RegisterResponce rr;

        private bool CheckUserNameFormat(string username)
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

        public bool CheckEqualPasswords(string password1, string password2)
        {
            return password1.Equals(password2);
        }

        public bool CheckUserNameUnused(string username)
        {
            return false;
        }

        public void Register(string username, string password, string password1, RegisterPage rp)
        {
            if (!CheckUserNameFormat(username))
            {
                rr.code = 101;
                rr.content.message = "用户名格式不正确";
                RegisterFailed();
                return;
            }
            if (!CheckPasswordFormat(password))
            {
                rr.code = 102;
                rr.content.message = "密码长度应为6—20";
                RegisterFailed();
                return;
            }
            if(!CheckEqualPasswords(password, password1))
            {
                rr.code = 103;
                rr.content.message = "两次输入的密码不相同";
                RegisterFailed();
                return;
            }

            password = GetMd5Hash(password);

            StringBuilder parameters = new StringBuilder();
            parameters.AppendFormat("{0}={1}", "username", HttpUtility.UrlEncode(username));
            parameters.AppendFormat("&{0}={1}", "password", HttpUtility.UrlEncode(password));

            Uri registerPath = new Uri(GlobalVars.WebServer + "/register");

            WebClient registerWC = new WebClient();
            registerWC.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            registerWC.Encoding = Encoding.UTF8;
            registerWC.UploadStringCompleted += (sender, e) =>
            {
                if (registerReturned)
                    return;
                registerReturned = true;
                if (e.Error != null)
                {
                    rr = new RegisterResponce() { code = 404, content = new Model.ResRegisterContent() { message = e.Error.Message } };
                }
                else
                {
                    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(e.Result));
                    //RegisterResult rr = new RegisterResult();
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RegisterResponce));
                    rr = (RegisterResponce)serializer.ReadObject(ms);
                }
                if (rr.code == 200)
                {
                    RegisterSuccess();
                }
                else
                {
                    RegisterFailed();
                }
            };
            registerWC.UploadStringAsync(registerPath, "POST", parameters.ToString());

            registerTimer.Tick += (sen, er) =>
            {
                if (registerReturned)
                    return;
                registerReturned = true;
                registerTimer.Stop();
                rr = new RegisterResponce() { code = 400, content = new Model.ResRegisterContent() { message = "连接服务器超时." } };
                RegisterFailed();
            };
            
            registerTimer.Start();
        }

        private void RegisterSuccess()
        {
            _registerPage.Dispatcher.BeginInvoke(new Action(() =>
            {
                ((Microsoft.Phone.Shell.ApplicationBarIconButton)_registerPage.ApplicationBar.Buttons[0]).IsEnabled = true;
                if (MessageBox.Show("注册成功") == MessageBoxResult.OK)
                    _registerPage.NavigationService.GoBack();
            }));
        }

        public void RegisterFailed()
        {
            _registerPage.Dispatcher.BeginInvoke(new Action(() =>
            {
                string msg = "注册失败. " + rr.content.message;
                ((Microsoft.Phone.Shell.ApplicationBarIconButton)_registerPage.ApplicationBar.Buttons[0]).IsEnabled = true;
                MessageBox.Show(msg);
            }));
        }

        public string GetMd5Hash(string input)
        {
            System.Text.UTF8Encoding decoder = new System.Text.UTF8Encoding();
            byte[] target = decoder.GetBytes(input);
            string output = BitConverter.ToString(MD5Core.GetHash(target));
            return output.Replace("-", "");
        }
    }
}

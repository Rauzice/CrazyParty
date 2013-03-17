using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace CrazyParty.BinSrc.Pages
{
    public partial class RegisterPage : PhoneApplicationPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        //click to submit a request of registering a new user
        private void registerSubmit_Click(object sender, EventArgs e)
        {
            string username = registerName.Text;
            string password = registerPassword.Password;
            string password1 = registerPassword1.Password;

            ((Microsoft.Phone.Shell.ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = false;
            GlobalVars.userRegisterHelper.Init();
            GlobalVars.userRegisterHelper.Register(username, password, password1, this);
        }

    }
}
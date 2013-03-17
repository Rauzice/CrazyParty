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
using System.Windows.Media.Imaging;
using CrazyParty.BinSrc;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Json;

namespace CrazyParty
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            #region init applicaitonBarIconButtons
            registerBtn = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            registerBtn.Click += new EventHandler(registerBtn_Click);
            registerBtn.IconUri = new Uri("/Images/appbar_button2.png", UriKind.Relative);
            registerBtn.Text = "注册";
            
            loginSubmit = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            loginSubmit.Click += new EventHandler(loginSubmit_Click);
            loginSubmit.IconUri = new Uri("/Images/appbar_button2.png", UriKind.Relative);
            loginSubmit.Text = "登录";

            startLocalGameBtn = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            startLocalGameBtn.Click += new EventHandler(startLocalGame);
            startLocalGameBtn.IconUri = new Uri("/Images/appbar_button2.png", UriKind.Relative);
            startLocalGameBtn.Text = "开始";

            startOnlineGameBtn = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            startOnlineGameBtn.Click += new EventHandler(startOnlineGame);
            startOnlineGameBtn.IconUri = new Uri("/Images/appbar_button2.png", UriKind.Relative);
            startOnlineGameBtn.Text = "开始";
            #endregion

            games = new List<CrazyPartyTile>();
            foreach (Control item in gameList.Items)
            {
                CrazyPartyTile tile = (CrazyPartyTile)item;
                games.Add(tile);
            }
            punishmens = new List<CrazyPartyTile>();
            foreach (Control item in punishmentList.Items)
            {
                CrazyPartyTile tile = (CrazyPartyTile)item;
                punishmens.Add(tile);
            }
        }

        public Microsoft.Phone.Shell.ApplicationBarIconButton loginSubmit, registerBtn;
        public Microsoft.Phone.Shell.ApplicationBarIconButton startLocalGameBtn, startOnlineGameBtn;

        List<CrazyPartyTile> games;
        List<CrazyPartyTile> punishmens;
        private int selectedGame = 0;
        private int selectedPunishment = 0;

        public int SelectedGame
        {
            get { return selectedGame; }
            set { selectedGame = value; }
        }
        public int SelectedPunishment
        {
            get { return selectedPunishment; }
            set { selectedPunishment = value; }
        }

        private void selectedGameTile(int gameNum)
        {
            foreach (CrazyPartyTile tile in games)
            {
                tile.Selected = false;
            }
            if (gameNum < games.Count && gameNum >= 0)
                games[gameNum].Selected = true;
        }

        private void selectedPunishmentTile(int punishmentNum)
        {
            foreach (CrazyPartyTile tile in punishmentList.Items)
            {
                tile.Selected = false;
            }
            if (punishmentNum < punishmens.Count && punishmentNum >= 0)
                punishmens[punishmentNum].Selected = true;
        }

        private void GameTile_Click(object sender, RoutedEventArgs e)
        {
            string senderName = ((Control)sender).Name;
            string numStr = senderName.Substring(4);
            if (string.IsNullOrEmpty(numStr))
                SelectedGame = 0;
            else
                SelectedGame = Convert.ToInt32(numStr);
            selectedGameTile(SelectedGame);
        }

        private void PunishmentTile_Click(object sender, RoutedEventArgs e)
        {
            string senderName = ((Control)sender).Name;
            string numStr = senderName.Substring(10);
            if (string.IsNullOrEmpty(numStr))
                SelectedPunishment = 0;
            else
                SelectedPunishment = Convert.ToInt32(numStr);
            selectedPunishmentTile(SelectedPunishment);
        }

        private void startLocalGame(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/BinSrc/Pages/BullsAndCows.xaml", UriKind.Relative));
        }

        private void startOnlineGame(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/BinSrc/Pages/BullsAndCows.xaml", UriKind.Relative));
        }


        //click to register page
        private void registerBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/BinSrc/Pages/RegisterPage.xaml", UriKind.Relative));
        }

        
        //click to submit a request of loging in a user
        private void loginSubmit_Click(object sender, EventArgs e)
        {
            ((Microsoft.Phone.Shell.ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = false;
            GlobalVars.userLoginHelper.Init();
            GlobalVars.userLoginHelper.Login(loginName.Text, loginPassword.Password, this);
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CPPivote.SelectedIndex == 2)
            {
                ApplicationBar.Buttons.Clear();
                ApplicationBar.Buttons.Add(loginSubmit);
                ApplicationBar.Buttons.Add(registerBtn);
            }
            else if(CPPivote.SelectedIndex == 0)
            {
                ApplicationBar.Buttons.Clear();
                ApplicationBar.Buttons.Add(startLocalGameBtn);
            }
            else if (CPPivote.SelectedIndex == 1)
            {
                ApplicationBar.Buttons.Clear();
                ApplicationBar.Buttons.Add(startOnlineGameBtn);
            }
        }
    }
}
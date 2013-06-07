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

            initApplicationBarIconButton();

            initHubTile();

            GlobalVars.liarDiceGameHelper = new LiarDiceGameHelper(this);
            GlobalVars.userLoginHelper = new UserLoginHelper(this);
        }

        #region ApplicationBarIconButton

        public Microsoft.Phone.Shell.ApplicationBarIconButton loginSubmitBtn, registerBtn;
        public Microsoft.Phone.Shell.ApplicationBarIconButton startLocalGameBtn, startOnlineGameBtn, refreshOnlineGameBtn, cancelOnlineGameBtn;

        private void initApplicationBarIconButton()
        {
            registerBtn = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            registerBtn.Click += new EventHandler(registerBtn_Click);
            registerBtn.IconUri = new Uri("/Icons/light/appbar.new.rest.png", UriKind.Relative);
            registerBtn.Text = "注册";

            loginSubmitBtn = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            loginSubmitBtn.Click += new EventHandler(loginSubmit_Click);
            loginSubmitBtn.IconUri = new Uri("/Icons/light/appbar.check.rest.png", UriKind.Relative);
            loginSubmitBtn.Text = "登录";

            startLocalGameBtn = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            startLocalGameBtn.Click += new EventHandler(startLocalGame);
            startLocalGameBtn.IconUri = new Uri("/Icons/light/appbar.play.rest.png", UriKind.Relative);
            startLocalGameBtn.Text = "开始";

            startOnlineGameBtn = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            startOnlineGameBtn.Click += new EventHandler(startOnlineGame);
            startOnlineGameBtn.IconUri = new Uri("/Icons/light/appbar.play.rest.png", UriKind.Relative);
            startOnlineGameBtn.Text = "开始";

            refreshOnlineGameBtn = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            refreshOnlineGameBtn.Click += new EventHandler(refreshOnlineGame);
            refreshOnlineGameBtn.IconUri = new Uri("/Icons/light/appbar.refresh.rest.png", UriKind.Relative);
            refreshOnlineGameBtn.Text = "刷新";

            cancelOnlineGameBtn = new Microsoft.Phone.Shell.ApplicationBarIconButton();
            cancelOnlineGameBtn.Click += new EventHandler(cancelOnlineGame);
            cancelOnlineGameBtn.IconUri = new Uri("/Icons/light/appbar.close.rest.png", UriKind.Relative);
            cancelOnlineGameBtn.Text = "取消";
            cancelOnlineGameBtn.IsEnabled = false;
        }

        private void startLocalGame(object sender, EventArgs e)
        {
            //
            //NavigationService.Navigate(new Uri("/BinSrc/Pages/BullsAndCows.xaml", UriKind.Relative));
            switch (SelectedGame)
            {
                case 0:
                    {
                        NavigationService.Navigate(new Uri("/BinSrc/Pages/SpinBottlePage.xaml", UriKind.Relative));
                        break;
                    }
                case 1:
                    {
                        NavigationService.Navigate(new Uri("/BinSrc/Pages/LocalDicingPage.xaml", UriKind.Relative));
                        break;
                    }
                case 2:
                    {
                        NavigationService.Navigate(new Uri("/BinSrc/Pages/BullsAndCows.xaml", UriKind.Relative));
                        break;
                    }
            }
        }

        private void startOnlineGame(object sender, EventArgs e)
        {
            if (GlobalVars.userid == 0)
            {
                MessageBox.Show("请先登录， 再开始游戏.");
                return;
            }

            startOnlineGameBtn.IsEnabled = false;
            cancelOnlineGameBtn.IsEnabled = true;
            GlobalVars.liarDiceGameHelper.connect();
        }

        private void refreshOnlineGame(object sender, EventArgs e)
        {
            GlobalVars.liarDiceGameHelper.refreshOnlineGameInfo();
        }

        private void cancelOnlineGame(object sender, EventArgs e)
        {
            startOnlineGameBtn.IsEnabled = true;
            cancelOnlineGameBtn.IsEnabled = false;
            GlobalVars.liarDiceGameHelper.Disconnect();
        }

        //click to register page
        private void registerBtn_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/BinSrc/Pages/RegisterPage.xaml", UriKind.Relative));
        }

        //click to submit a request of loging in a user
        private void loginSubmit_Click(object sender, EventArgs e)
        {
            loginSubmitBtn.IsEnabled = false;
            GlobalVars.userLoginHelper.Login(loginName.Text, loginPassword.Password, this);
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CPPivote.SelectedIndex == 2)
            {
                ApplicationBar.Buttons.Clear();
                ApplicationBar.Buttons.Add(loginSubmitBtn);
                ApplicationBar.Buttons.Add(registerBtn);
            }
            else if (CPPivote.SelectedIndex == 0)
            {
                ApplicationBar.Buttons.Clear();
                ApplicationBar.Buttons.Add(startLocalGameBtn);
            }
            else if (CPPivote.SelectedIndex == 1)
            {
                ApplicationBar.Buttons.Clear();
                ApplicationBar.Buttons.Add(startOnlineGameBtn);
                ApplicationBar.Buttons.Add(cancelOnlineGameBtn);
                ApplicationBar.Buttons.Add(refreshOnlineGameBtn);
            }
        }

        #endregion

        #region HubTile

        List<LocalGameTile> games;
        List<LocalGameTile> punishmens;
        List<OnlineGameTile> onlineGames;
        private int selectedGame = 0;
        private int selectedPunishment = 0;
        private int selectedOnlineGame = 0;

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

        public int SelectedOnlineGame
        {
            get { return selectedOnlineGame; }
            set { selectedOnlineGame = value; }
        }

        private void initHubTile()
        {
            games = new List<LocalGameTile>();
            foreach (Control item in gameList.Items)
            {
                LocalGameTile tile = (LocalGameTile)item;
                games.Add(tile);
            }
            punishmens = new List<LocalGameTile>();
            foreach (Control item in punishmentList.Items)
            {
                LocalGameTile tile = (LocalGameTile)item;
                punishmens.Add(tile);
            }

            onlineGames = new List<OnlineGameTile>();
            foreach (Control item in onlineGameList.Items)
            {
                OnlineGameTile tile = (OnlineGameTile)item;
                onlineGames.Add(tile);
            }
        }

        private void selectedGameTile(int gameNum)
        {
            foreach (LocalGameTile tile in games)
            {
                tile.Selected = false;
            }
            if (gameNum < games.Count && gameNum >= 0)
                games[gameNum].Selected = true;
        }

        private void selectedPunishmentTile(int punishmentNum)
        {
            foreach (LocalGameTile tile in punishmentList.Items)
            {
                tile.Selected = false;
            }
            if (punishmentNum < punishmens.Count && punishmentNum >= 0)
                punishmens[punishmentNum].Selected = true;
        }

        private void selectedOnlineGameTile(int gameNum)
        {
            foreach (OnlineGameTile tile in onlineGames)
            {
                tile.Selected = false;
            }
            if (gameNum < onlineGames.Count && gameNum >= 0)
                onlineGames[gameNum].Selected = true;
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

        private void OnlineGameTile_Click(object sender, RoutedEventArgs e)
        {
            string senderName = ((Control)sender).Name;
            string numStr = senderName.Substring(10);
            if (string.IsNullOrEmpty(numStr))
                SelectedOnlineGame = 0;
            else
                SelectedOnlineGame = Convert.ToInt32(numStr);
            selectedOnlineGameTile(SelectedOnlineGame);
        }

        #endregion


        #region public UI update

        public bool GotoOnlinePage()
        {
            NavigationService.Navigate(new Uri(@"/BinSrc/Pages/LiarDicePage.xaml", UriKind.Relative));
            return true;
        }

        public void UpdateOnlineGameInfo(int playingCount, int waitingCount)
        {
        }

        #endregion
    }
}
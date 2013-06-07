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

namespace CrazyParty.BinSrc
{
    public class OnlineGameTile: Button
    {
        #region Title property

        public static readonly DependencyProperty TitleProperty = 
            DependencyProperty.Register("Title", typeof(string), typeof(OnlineGameTile), null);

        public string Title
        {
            get{ return (string)GetValue(TitleProperty); }
            set{ SetValue(TitleProperty, value); }
        }

        #endregion

        #region Abstract property

        public static readonly DependencyProperty AbstractProperty = 
            DependencyProperty.Register("Abstract", typeof(string), typeof(OnlineGameTile), null);

        public string Abstract
        {
            get { return (string)GetValue(AbstractProperty); }
            set { SetValue(AbstractProperty, value); }
        }

        #endregion

        #region GameImage property

        public static readonly DependencyProperty GameImageProperty =
            DependencyProperty.Register("GameImage", typeof(ImageSource), typeof(OnlineGameTile), null);

        public ImageSource GameImage
        {
            get { return (ImageSource)GetValue(GameImageProperty); }
            set { SetValue(GameImageProperty, value); }
        }

        #endregion

        public OnlineGameTile()
        {
            DefaultStyleKey = typeof(OnlineGameTile);
        }

        #region Selected Property

        public static readonly DependencyProperty SelectedProperty =
         DependencyProperty.Register("Selected", typeof(bool), typeof(OnlineGameTile), new PropertyMetadata(false));

        public bool Selected
        {
            get { return (bool)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }

        #endregion

        #region TotalPlayers Property

        public static readonly DependencyProperty TotalPlayersProperty =
         DependencyProperty.Register("TotalPlayers", typeof(int), typeof(OnlineGameTile), new PropertyMetadata(0));

        public int TotalPlayers
        {
            get { return (int)GetValue(TotalPlayersProperty); }
            set { SetValue(TotalPlayersProperty, value); }
        }
        #endregion

        #region WaitingPlayers Property

        public static readonly DependencyProperty WaitingPlayersProperty =
         DependencyProperty.Register("WaitingPlayers", typeof(int), typeof(OnlineGameTile), new PropertyMetadata(0));

        public int WaitingPlayers
        {
            get { return (int)GetValue(WaitingPlayersProperty); }
            set { SetValue(WaitingPlayersProperty, value); }
        }

        #endregion

        #region IsJoined Property
        public static readonly DependencyProperty IsJoinedProperty =
         DependencyProperty.Register("IsJoined", typeof(bool), typeof(OnlineGameTile), new PropertyMetadata(false));

        public bool IsJoined
        {
            get { return (bool)GetValue(IsJoinedProperty); }
            set { SetValue(IsJoinedProperty, value); }
        }

        #endregion

        #region JoinTime Property
        public static readonly DependencyProperty JoinTimeProperty =
         DependencyProperty.Register("JoinTime", typeof(DateTime), typeof(OnlineGameTile), new PropertyMetadata(DateTime.Now));

        public DateTime JoinTime
        {
            get { return (DateTime)GetValue(JoinTimeProperty); }
            set { SetValue(JoinTimeProperty, value); }
        }

        #endregion

        #region WaitingTime Property
        public static readonly DependencyProperty WaitingTimeProperty =
         DependencyProperty.Register("WaitingTime", typeof(int), typeof(OnlineGameTile), new PropertyMetadata(0));

        public int WaitingTime
        {
            get{ return (int)GetValue(WaitingTimeProperty); }
            set { SetValue(WaitingTimeProperty, value); }
        }
        #endregion
    }
}

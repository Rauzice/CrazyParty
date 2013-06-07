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

namespace CrazyParty.BinSrc.Model
{
    public class OnlineGameInfo
    {
        public OnlineGameInfo(string name, int tp, int wp, DateTime jt, string ab)
        {
            GameName = name;
            TotalPayer = tp;
            WaitingPlayer = wp;
            JoinTime = jt;
            IsJoined = false;
            Abstract = ab;
        }

        public OnlineGameInfo(string name)
        {
            TotalPayer = 0;
            WaitingPlayer = 0;
            JoinTime = DateTime.Now;
            IsJoined = false;
            Abstract = "";
        }

        private string _gameName;
        public string GameName
        {
            set { _gameName = value; }
            get { return _gameName; }
        }

        //当前在线游戏玩家数
        private int _totalPlayer;
        public int TotalPayer
        {
            set { _totalPlayer = value; }
            get { return _totalPlayer; }
        }


        //当前在线等待游戏玩家数
        private int _waitingPlayer;
        public int WaitingPlayer
        {
            set { _waitingPlayer = value; }
            get { return _waitingPlayer; }
        }

        //排队时间
        private DateTime _joinTime;
        public DateTime JoinTime
        {
            set { _joinTime = value; }
            get { return _joinTime; }
        }

        public TimeSpan WaitingTime
        {
            get { return DateTime.Now - JoinTime; }
        }

        //是否加入
        private bool _isJoined;
        public bool IsJoined
        {
            get { return _isJoined; }
            set { _isJoined = value; }
        }

        //游戏简介
        private string _abstract;
        public string Abstract
        {
            get { return _abstract; }
            set { _abstract = value; }
        }
    }
}

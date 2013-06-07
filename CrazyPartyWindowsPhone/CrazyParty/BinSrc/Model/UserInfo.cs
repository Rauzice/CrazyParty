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
using System.Runtime.Serialization;

namespace CrazyParty.BinSrc.Model
{
    public enum PersonGender
    {
        male,
        female
    };

    [DataContract]
    public class UserInfo
    {
        public UserInfo()
        {
            Username = "";
            Email = "";
            From = "";
            State = 0;
            LoginCount = 0;
            LastLoginTime = new DateTime(2000, 1, 1, 0, 0, 0) ;
            Moment = "";
            Birthday = new DateTime(2000, 1, 1);
            Gender = "male";
            Avatar = "";
            LiarDiceTotalCount = 0;
            LiarDiceWinCount = 0;
            DicingTotalCount = 0;
            DicingWinCount = 0;
        }

        public UserInfo(UserInfo user)
        {
            Username = user.Username;
            Email = user.Email;
            From = user.From;
            State = user.State;
            LoginCount = user.LoginCount;
            LastLoginTime = user.LastLoginTime;
            Moment = user.Moment;
            Birthday = user.Birthday;
            Gender = user.Gender;
            Avatar = user.Avatar;
            LiarDiceTotalCount = user.LiarDiceTotalCount;
            LiarDiceWinCount = user.LiarDiceWinCount;
            DicingTotalCount = user.DicingTotalCount;
            DicingWinCount = user.DicingWinCount;
        }

        public UserInfo(string username, string email, string from, int state, int loginCount,
            DateTime lastLoginTime, string moment, DateTime birthday, string gender, string avatar,
            int liarDiceTotalCount, int liarDiceWinCount, int dicingTotalCount, int dicingWinCount)
        {
            Username = username;
            Email = email;
            From = from;
            State = state;
            LoginCount = loginCount;
            LastLoginTime = lastLoginTime;
            Moment = moment;
            Birthday = birthday;
            Gender = gender;
            Avatar = avatar;
            LiarDiceTotalCount = liarDiceTotalCount;
            LiarDiceWinCount = liarDiceWinCount;
            DicingTotalCount = dicingTotalCount;
            DicingWinCount = dicingWinCount;
        }

        [DataMember]
        public int UserId
        {
            get;
            set;
        }

        [DataMember]
        public string Username
        {
            get;
            set;
        }

        [DataMember]
        public string Email
        {
            get;
            set;
        }

        [DataMember]
        public string From
        {
            get;
            set;
        }

        [DataMember]
        public int State
        {
            get;
            set;
        }

        [DataMember]
        public string Gender
        {
            get;
            set;
        }

        [DataMember]
        public DateTime Birthday
        {
            get;
            set;
        }

        [DataMember]
        public string Avatar
        {
            get;
            set;
        }

       [DataMember]
        public int LoginCount
        {
            get;
            set;
        }

        [DataMember]
        public DateTime LastLoginTime
        {
            get;
            set;
        }

        [DataMember]
        public string Moment
        {
            get;
            set;
        }

        [DataMember]
        public int LiarDiceTotalCount
        {
            get;
            set;
        }

        [DataMember]
        public int LiarDiceWinCount
        {
            get;
            set;
        }

        [DataMember]
        public int DicingTotalCount
        {
            get;
            set;
        }

        [DataMember]
        public int DicingWinCount
        {
            get;
            set;
        }
    }
}

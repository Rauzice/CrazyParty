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
using CrazyParty.BinSrc.Model;

namespace CrazyParty.BinSrc
{
    public class GlobalVars
    {
        public static string WebServer = @"http://192.168.1.102:3001";
        public static string GameServer = @"http://192.168.1.102:3456";
        public static string GameWSServer = @"ws://192.168.1.102:3005";

        public static UserRegisterHelper userRegisterHelper = new UserRegisterHelper();
        public static UserLoginHelper userLoginHelper = new UserLoginHelper();
        public static UserProfileHelper userProfileHelper = new UserProfileHelper();

        public static GameHelper gameHelper;

        public static UserInfo userInfo = new UserInfo();
    }
}

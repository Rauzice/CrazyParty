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
using System.IO.IsolatedStorage;

namespace CrazyParty.BinSrc
{
    public class GlobalVars
    {
        public static uint userid = 0;

        public static string WebServer = @"http://192.168.1.102:3001";
        public static string GameWebServer = @"http://192.168.1.102:3010";
        public static string GameWSServer = @"ws://192.168.1.102:3456";

        public static string LiarDiceGameName = "liardice";
        public static string OnlineDicingGameName = "dicing";

        public static UserRegisterHelper userRegisterHelper = new UserRegisterHelper();
        public static UserLoginHelper userLoginHelper;
        public static UserProfileHelper userProfileHelper = new UserProfileHelper();

        public static LiarDiceGameHelper liarDiceGameHelper;

        public static UserInfo userInfo = new UserInfo();

        public class RequestCode
        {
            public static int JoinCode = 100;
            public static int CancelJoinCode = 101;


            public static int SyscCode = 800;
        }

        public class ResponceCode
        {
            public static int JoinSuccess = 100;
            public static int JoinFailed = 101;

            public static int AllocateSuccess = 200;
            public static int AllocateFailed = 201;

            public static int ExitSuccess = 300;
            public static int ExitFailed = 301;

            public static int SyscCode = 800;
        }

        public class LiarDiceGameCode
        {
            public static int PrepareCode = 1;
            public static int DiceCode = 2;
            public static int LiarCode = 3;
            public static int ShowCode = 4;
        }
    }
}

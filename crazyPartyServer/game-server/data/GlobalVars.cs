﻿using System;
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
        public static string GameWSServer = @"ws://192.168.1.102:3456";

        public static UserRegisterHelper userRegisterHelper = new UserRegisterHelper();
        public static UserLoginHelper userLoginHelper = new UserLoginHelper();
        public static UserProfileHelper userProfileHelper = new UserProfileHelper();

        public static GameHelper gameHelper;

        public static UserInfo userInfo = new UserInfo();

        public class RequestCode
        {
            public static int JoinCode = 100;
            public static int CancelJoinCode = 101;
            public static int PrepareCode = 200;
            public static int RockResultCode = 300;
            public static int StateCode = 400;
            public static int ExitCode = 500;

            public static int SyscCode = 800;
        }

        public class ResponceCode
        {
            public static int JoinSuccess = 100;
            public static int JoinFailed = 101;

            public static int AllocateSuccess = 200;
            public static int AllocateFailed = 201;

            public static int StartGameSuccess = 300;
            public static int StartGameFailed = 301;

            public static int EndGameSuccess = 400;
            public static int EndGameFailed = 401;

            public static int GameState = 500;

            public static int SyscCode = 800;
        }
    }
}

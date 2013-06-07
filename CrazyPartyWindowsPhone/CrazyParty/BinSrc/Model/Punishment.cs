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
using System.IO;

namespace CrazyParty.BinSrc.Model
{
    public class Punishment
    {
        const int Max_Size = 1000;
        #region 真心话和大冒险惩罚选项
        private static string[] trueWordItems = {"和男/女朋友进行到哪一步了", "你作弊使用过的最高招",
                                 "最喜欢在座哪位异性", "结婚后希望生男孩还是女孩(只能选择一个，说出原因)。",
                                 "内裤颜色", "每天睡觉前都会想起的人是谁?",
                                 "初吻年龄", "让你一直念念不忘的一位异性的名字?原因?",
                                 "自己最丢人的事", "你曾经意淫过在场的哪一位?如果过去没有的话，你现在会选哪一位作为YY对象?",
                                 "最后一次发自内心的笑是什么时候？", "所有考试中，你考到最低的一门是什么课，考了几分?",
                                 "愿意为爱情牺牲到什么程度", "如果看到自己最爱的人熟睡在你面前你会做什么?",
                                 "朋友和男/女朋友那个重要", "当你最不知道穿什么颜色的时候，你会选择什么颜色?",
                                 "身上哪个部位最敏感", "如果从天而降99枚金币，你的第一反应是什么?",
                                 "如果有来生，你选择当？", "当你被我点名的时候是什么心情?(不要骗我们哦~~)",
                                 "你会选择Having sex before marriage吗？", "你觉得最美的画面是什么?",
                                 "如果让你选择做一个电影中的角色，你会选谁呢？",
                                 "如果有一天我和你吵架，你会怎么办？",
                                 "哭得最伤心的是哪一次？为什么？",
                                 "如果跟你喜欢的人约会，碰到前任的男（女）朋友，会有什么表现？",
                                 "如果有一天我对你说我爱上你了，你怎么办？"};

        private static string[] budRedHeadItems = { "背一位异性绕场一周", "唱青藏高原最后一句", "做一个大家都满意的鬼脸",
                                     "抱一位异性直到下一轮真心话大冒险结束", "像一位异性表白3分钟",
                                     "与一位异性十指相扣，对视10秒", "邀请一位异性为你唱情歌", "或邀请一位异性与你情歌对唱",
                                     "做自己最性感、最妩媚的表情或动作", "吃下每个人为你夹得菜", "跳z脱衣舞",
                                     "蹲在凳子上作便秘状",  "亲一位异性，部位不限", "深情的吻墙10秒",
                                     "模仿古代特殊职业女子拉客", "模仿脑白金广告，边唱边跳", 
                                     "到街上大喊“卖拖鞋啦～2块一双，买一送3“", "抓着铁门喊“放我出去！”",   
                                     "对陌生人美眉挤眉弄眼", "换零钱~十元去换一百个一角", "打10086问服务小姐，“我家的钥匙被猫吞了该怎么办？",
                                     "去外面对着电线杆边大声说，我的病 有 救 了~ ","(男)拿张餐巾纸，靠到门口，边挥边对每个路过的人说“大爷进来玩啊……”",
                                     "脱下自己的一只鞋子，拿着那只鞋子当电话打，还是要装得真和别人讲电话一样.", "去角落那桌问他们，可不可以给我吃一口，然后吃一口。",
                                     "给大家都认识的一个人打电话：＂ＸＸ，其实我是同性恋，我一直在默默地爱着", "对服务生说：海狸先生，为什么你的牙齿那么美白？ ",
                                   "正面对着十指交扣，深情对视，深情朗诵骆宾王 的《鹅》"};
        #endregion

        public Punishment()
        {
        }

        public static string GetPunishment()
        {
            int len = trueWordItems.Length + budRedHeadItems.Length;
            Random ran = new Random();
            int x = ran.Next(len);
            if (x < trueWordItems.Length)
                return trueWordItems[x];
            else
                return budRedHeadItems[x - trueWordItems.Length];
        }
    }
}

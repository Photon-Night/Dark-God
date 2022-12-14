using PENet;
using System;

namespace PEProtocol
{
    [System.Serializable]
    public class GameMsg : PEMsg
    {
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
        public ReqRename reqRename;
        public RspRename rspRename;
        public ReqGuide reqGuide;
        public RspGuide rspGuide;
        public ReqStrong reqStrong;
        public RspStrong rspStrong;
        public SendChat sendChat;
        public PushChat pushCHat;
        public ReqBuy reqBuy;
        public RspBuy rspBuy;
        public PushPower pushPower;
        public ReqTakeTaskReward reqTakeTaskReward;
        public RspTakeTaskReward rspTakeTaskReward;
        public PushTaskPrgs pushTaskPrgs;
        public ReqMissionEnter reqMissionEnter;
        public RspMissionEnter rspMissionEnter;
        public ReqCheckConnection reqCheckConnection;
        public RspCheckConnection rspCheckConnection;
        public ReqFBFightEnd reqFBFightEnd;
        public RspFBFightEnd rspFBFightEnd;
    }

    public class ServerCfg
    {
        public const string srvIP = "127.0.0.1";
        public const int srvPort = 10086;
    }

    [System.Serializable]
    public class ReqLogin
    {
        public string acc;
        public string pas;
    }
    [System.Serializable]
    public class RspLogin
    {
        //TODO
        public PlayerData playerData;
    }

    [System.Serializable]
    public class ReqRename
    {
        public string name;
    }

    [System.Serializable]
    public class RspRename
    {
        public string name;
    }

    [System.Serializable]

    public class ReqGuide
    {
        public int guideid;
    }

    [System.Serializable]
    public class RspGuide
    {
        public int guideid;
        public int coin;
        public int lv;
        public int exp;
    }

    [System.Serializable]
    public class ReqStrong
    {
        public int pos;
    }

    [System.Serializable]
    public class RspStrong
    {
        public int coin;
        public int crystal;
        public int hp;
        public int ad;
        public int ap;
        public int addef;
        public int apdef;
        public int[] strong;
    }


    [System.Serializable]
    public class SendChat
    {
        public string chat;
    }

    [System.Serializable]
    public class PushChat
    {
        public string name;
        public string chat;
    }

    [System.Serializable]
    public class ReqBuy
    {
        public int buyType;
        public int diamond;
    }

    [System.Serializable]
    public class RspBuy
    {
        public int buyType;
        public int diamond;
        public int power;
        public int coin;
    }

    [System.Serializable]
    public class PushPower
    {
        public int power;
    }

    [System.Serializable]
    public class ReqTakeTaskReward
    {
        public int rid;
    }

    [System.Serializable]
    public class RspTakeTaskReward
    {
        public int coin;
        public int exp;
        public int lv;
        public string[] taskArr;
    }

    [System.Serializable]
    public class PushTaskPrgs
    {
        public string[] taskArr;
    }

    [System.Serializable]
    public class ReqMissionEnter
    {
        public int mid;
    }

    [System.Serializable]
    public class RspMissionEnter
    {
        public int mid;
        public int power;
    }

    [System.Serializable]
    public class ReqCheckConnection
    {
        public int id;
    }

    [System.Serializable]
    public class RspCheckConnection
    {
        public int id;
    }

    #region 副本战斗相关

    [Serializable]
    public class ReqFBFightEnd
    {
        public bool win;
        public int fbid;
        public int resthp;
        public int costtime;
    }

    [Serializable]
    public class RspFBFightEnd
    {
        public bool win;
        public int fbid;
        public int resthp;
        public int costtime;

        //副本奖励
        public int coin;
        public int lv;
        public int exp;
        public int crystal;
        public int mission;
    }
    #endregion

    [System.Serializable]
    public class PlayerData
    {
        public int id;
        public string name;
        public int lv;
        public int exp;
        public int power;
        public int coin;
        public int diamond;

        public int hp;
        public int ad;
        public int ap;
        public int addef;
        public int apdef;
        public int dodge;//闪避概率
        public int pierce;//穿透比率
        public int critical;//暴击概率

        public int guideid;
        public int[] strong;
        public int crystal;
        public int mission;

        public long time;

        public string[] task;
    }


    public enum CMD
    {
        None = 0,

        ReqLogin = 101,
        RspLogin = 102,
        ReqRename = 103,
        RspRename = 104,

        ReqGuide = 201,
        RspGuide = 202,
        ReqStrong = 203,
        RspStrong = 204,

        SendChat = 205,
        PushChat = 206,

        ReqBuy = 207,
        RspBuy = 208,

        PushPower = 209,
        ReqTakeTaskReward = 210,
        RspTakeTaskReward = 211,
        PushTaskPrgs = 212,


        ReqMissionEnter = 301,
        RspMissionEnter = 302,

        ReqFBFightEnd = 303,
        RspFBFightEnd = 304,

        ReqCheckConnection = 401,
        RspCheckConnection = 402,
    }

    public enum ErrorCode
    {
        None = 0,
        AccountIsOnline,
        WrongPass,
        NameIsExist,

        UpdateDBError,
        ServerDataError,

        LackLevel,
        LackCoin,
        LackCrystal,
        LackDiamond,
        ClientDataError,

        LackPower,
    }


}

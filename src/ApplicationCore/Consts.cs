using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
{
    public static class AppSettingsKey
    {
        public const string SecurityKey = "SecurityKey";
        public const string Mode = "Mode";
        public const string LogFile = "LogFile";
        public const string Begin = "Begin";
        public const string End = "End";
        public const string SID = "SID";
        public const string Password = "Password";
        public const string OrderMaker = "OrderMaker";
        public const string OrderMakerIP = "OrderMakerIP";
    }
    public enum BrokageName
    {
        CONCORD = 0,
        HUA_NAN = 1,
        ONRICH = 2,
        CAPITAL = 3,
        BINANCE = 4,
        FAKE = 5
    }

    public enum ConnectionStatus
    {
        DISCONNECTED = 0,
        CONNECTING = 1,
        CONNECTED = 2
    }


}

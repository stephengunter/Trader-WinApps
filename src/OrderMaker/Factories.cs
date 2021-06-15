using ApplicationCore.Helpers;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using ApplicationCore.Brokages.Capital;
using ApplicationCore.Brokages.Fake;
using ApplicationCore.OrderMaker.Managers;
using ApplicationCore.OrderMaker.Models;
using NLog;
using ApplicationCore;
using ApplicationCore.Brokages;
using ApplicationCore.Brokages.Concord;

namespace OrderMaker
{
    public class Factories
    {
        public static ISettingsManager CreateSettingsManager() => new SettingsManager();
        public static ITimeManager CreateTimeManager(string begin, string end) => new TimeManager(begin, end);
        public static IPositionManager CreatePositionManager(IOrderMaker orderMaker, TradeSettings tradeSettings, ILogger logger)
        {
            if (orderMaker.Name == BrokageName.HUA_NAN) return new AsyncPositionManager(orderMaker, tradeSettings, logger);
            
            return new SyncPositionManager(orderMaker, tradeSettings, logger);
        }
        public static IOrderMaker CreateOrderMaker(string name, BrokageSettings settings)
        {
            if (name.EqualTo(BrokageName.FAKE.ToString())) return new FakeBrokage(settings);
            else if (name.EqualTo(BrokageName.CAPITAL.ToString())) return new CapitalBrokage(settings);
            else return new ConcordBrokage(settings);
        }

    }
}

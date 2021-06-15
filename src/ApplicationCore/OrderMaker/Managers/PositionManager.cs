using ApplicationCore.OrderMaker;
using ApplicationCore.OrderMaker.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker.Managers
{
    public interface IPositionManager
    {
        void SyncPosition(IPositionInfo positionInfo);
        void FetchDeals();
    }

    public abstract class BasePositionManager
    {
        private readonly IOrderMaker _orderMaker;
        private readonly TradeSettings _tradeSettings;
        private readonly NLog.ILogger _logger;

        public BasePositionManager(IOrderMaker orderMaker, TradeSettings tradeSettings, ILogger logger)
        {
            _orderMaker = orderMaker;
            _tradeSettings = tradeSettings;
            _logger = logger;
        }

        protected IOrderMaker OrderMaker => _orderMaker;
        protected TradeSettings TradeSettings => _tradeSettings;
        protected NLog.ILogger Logger => _logger;

        

        #region IPositionManager Functions
        public abstract void SyncPosition(IPositionInfo positionInfo);

        public void FetchDeals()
        {
            foreach (var accountSettings in _tradeSettings.Accounts)
            {
                _orderMaker.FetchDeals(accountSettings.Account);
            }

        }
        #endregion

        protected void MakeOrder(string symbol, string account, decimal marketPrice, int lots, bool dayTrade)
        {
            int offset = _tradeSettings.Offset;

            if (lots > 0) _orderMaker.MakeOrder(symbol, account, marketPrice + offset, lots, dayTrade); //買進
            else if (lots < 0) _orderMaker.MakeOrder(symbol, account, marketPrice - offset, lots, dayTrade); //賣出
        }

    }

    public class SyncPositionManager : BasePositionManager, IPositionManager
    {
        public SyncPositionManager(IOrderMaker orderMaker, TradeSettings tradeSettings, ILogger logger)
            : base(orderMaker, tradeSettings, logger)
        {
            logger.Info("PositionManager Created. Type:SyncPositionManager");
        }
        List<AccountSettings> Accounts => TradeSettings.Accounts;
        public override void SyncPosition(IPositionInfo positionInfo)
        {
            foreach (var accountSettings in Accounts)
            {
                string symbol = accountSettings.Symbol;
                string accNum = accountSettings.Account;

                int currentLots = OrderMaker.GetAccountPositions(accNum, symbol);
                int correctLots = positionInfo.Position * accountSettings.Lot;

                int lotsNeedOrder = correctLots - currentLots;
                if (lotsNeedOrder != 0)
                {
                    decimal marketPrice = positionInfo.MarketPrice;
                    bool dayTrade = TradeSettings.DayTrade;
                    MakeOrder(symbol, accNum, marketPrice, lotsNeedOrder, dayTrade);
                }

                Logger.Info($"SyncPosition: {positionInfo.Position}({positionInfo.MarketPrice}), Account:{accNum} ({symbol}, {accountSettings.Lot}), OI: {currentLots}, Need: {lotsNeedOrder}");
            }
        }
    }

    public class AsyncPositionManager : BasePositionManager, IPositionManager
    {
        IPositionInfo _positionInfo = null;
        public AsyncPositionManager(IOrderMaker orderMaker, TradeSettings tradeSettings, ILogger logger)
            :base(orderMaker, tradeSettings, logger)
        {

            OrderMaker.AccountPositionUpdated += OrderMaker_AccountPositionUpdated;
            logger.Info("PositionManager Created. Type:SyncPositionManager");
        }

        public override void SyncPosition(IPositionInfo positionInfo)
        {
            if (positionInfo.MarketPrice <= 0) return;

            _positionInfo = positionInfo;

            InitAccountPositionStatus();

            BeginSync();
        }
        private void OrderMaker_AccountPositionUpdated(object sender, EventArgs e)
        {

            try
            {
                var args = e as AccountEventArgs;
                SyncPosition(args.Account);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

            }
        }
        void SyncPosition(string account)
        {
            if (_positionInfo == null) return;
            var accountSettings = TradeSettings.FindAccountSettings(account);
            if (accountSettings == null) return;

            string symbol = accountSettings.Symbol;

            int currentLots = OrderMaker.GetAccountPositions(account, symbol);
            int correctLots = _positionInfo.Position * accountSettings.Lot;

            int lotsNeedOrder = correctLots - currentLots;
            Logger.Info($"SyncPosition: {_positionInfo.Position}({_positionInfo.MarketPrice}), Account:{account} ({symbol}, {accountSettings.Lot}), OI: {currentLots}, Need: {lotsNeedOrder}");

            if (lotsNeedOrder == 0)
            {
                OrderMaker.ClearOrders(symbol, account);

                var accountStatus = _accountPositionStatuses.FirstOrDefault(x => x.Id == account);
                accountStatus.Sync = true;

                BeginSync();//處理下一個帳號
            }
            else
            {

                decimal marketPrice = _positionInfo.MarketPrice;
                bool dayTrade = TradeSettings.DayTrade;

                MakeOrder(symbol, account, marketPrice, lotsNeedOrder, dayTrade);
            }

        }

        List<AccountPositionStatus> _accountPositionStatuses = new List<AccountPositionStatus>();
        void InitAccountPositionStatus()
        {
            _accountPositionStatuses = new List<AccountPositionStatus>();
            foreach (var accountSettings in TradeSettings.Accounts)
            {
                _accountPositionStatuses.Add(new AccountPositionStatus { Id = accountSettings.Account, Sync = false });
            }
        }
        void BeginSync()
        {
            var accountStatus = _accountPositionStatuses.FirstOrDefault(x => !x.Sync);
            if (accountStatus == null) return;

            OrderMaker.RequestAccountPositions(accountStatus.Id);

        }

        class AccountPositionStatus
        {
            public string Id { get; set; }
            public bool Sync { get; set; }
        }
    }

    



}

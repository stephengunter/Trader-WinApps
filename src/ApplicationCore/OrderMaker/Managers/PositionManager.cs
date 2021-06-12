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


    public class PositionManager : IPositionManager
    {
        private readonly IOrderMaker _orderMaker;
        private readonly TradeSettings _tradeSettings;
        private readonly NLog.ILogger _logger;

        IPositionInfo _positionInfo = null;

        public PositionManager(IOrderMaker orderMaker, TradeSettings tradeSettings, ILogger logger)
        {
            _orderMaker = orderMaker;
            _tradeSettings = tradeSettings;
            _logger = logger;

            if (_orderMaker.Name == BrokageName.HUA_NAN)
            {
                _orderMaker.AccountPositionUpdated += OrderMaker_AccountPositionUpdated;
            }
        }

        #region  Helper
        List<AccountSettings> Accounts => _tradeSettings.Accounts;
        #endregion

        #region IPositionManager Functions
        public void SyncPosition(IPositionInfo positionInfo)
        {
            if (positionInfo.MarketPrice <= 0) return;

            if (_orderMaker.Name == BrokageName.HUA_NAN)
            {
                _positionInfo = positionInfo;

                InitAccountPositionStatus();

                BeginSync();
            }
            else
            {
                foreach (var accountSettings in Accounts)
                {
                    string symbol = accountSettings.Symbol;
                    int lotsNeedOrder = GetLotsNeedOrder(positionInfo, accountSettings);
                    if (lotsNeedOrder != 0)
                    {
                        decimal marketPrice = positionInfo.MarketPrice;
                        bool dayTrade = _tradeSettings.DayTrade;
                        MakeOrder(symbol, accountSettings.Account, marketPrice, lotsNeedOrder, dayTrade);
                    }
                }
            }
        }

        public void FetchDeals()
        {
            foreach (var accountSettings in _tradeSettings.Accounts)
            {
                _orderMaker.FetchDeals(accountSettings.Account);
            }

        }
        #endregion

        private void OrderMaker_AccountPositionUpdated(object sender, EventArgs e)
        {
            try
            {
                var args = e as AccountEventArgs;
                SyncPosition(args.Account);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
        }

        List<AccountPositionStatus> _accountPositionStatuses = new List<AccountPositionStatus>();
        void InitAccountPositionStatus()
        {
            _accountPositionStatuses = new List<AccountPositionStatus>();
            foreach (var accountSettings in _tradeSettings.Accounts)
            {
                _accountPositionStatuses.Add(new AccountPositionStatus { Id = accountSettings.Account, Sync = false });
            }
        }
        void BeginSync()
        {
            var accountStatus = _accountPositionStatuses.FirstOrDefault(x => !x.Sync);
            if (accountStatus == null) return;

            _orderMaker.RequestAccountPositions(accountStatus.Id);

        }





        void SyncPosition(string account)
        {
            if (_positionInfo == null) return;
            var accountSettings = _tradeSettings.FindAccountSettings(account);
            if (accountSettings == null) return;

            string symbol = accountSettings.Symbol;
            int lotsNeedOrder = GetLotsNeedOrder(_positionInfo, accountSettings);
            if (lotsNeedOrder == 0)
            {
                _orderMaker.ClearOrders(symbol, account);

                var accountStatus = _accountPositionStatuses.FirstOrDefault(x => x.Id == account);
                accountStatus.Sync = true;

                BeginSync();//處理下一個帳號
            }
            else
            {

                decimal marketPrice = _positionInfo.MarketPrice;
                bool dayTrade = _tradeSettings.DayTrade;

                MakeOrder(symbol, account, marketPrice, lotsNeedOrder, dayTrade);
            }

        }

        int GetLotsNeedOrder(IPositionInfo positionInfo, AccountSettings accountSettings)
        {
            string symbol = accountSettings.Symbol;
            string account = accountSettings.Account;

            int currentLots = _orderMaker.GetAccountPositions(account, symbol);

            int correctLots = positionInfo.Position * accountSettings.Lot;

            return correctLots - currentLots;
        }

        void MakeOrder(string symbol, string account, decimal marketPrice, int lots, bool dayTrade)
        {
            int offset = _tradeSettings.Offset;

            if (lots > 0) _orderMaker.MakeOrder(symbol, account, marketPrice + offset, lots, dayTrade); //買進
            else if (lots < 0) _orderMaker.MakeOrder(symbol, account, marketPrice - offset, lots, dayTrade); //賣出
        }


        class AccountPositionStatus
        {
            public string Id { get; set; }
            public bool Sync { get; set; }
        }

        class AccountDealStatus
        {
            public string Id { get; set; }
            public bool Fetched { get; set; }
        }

    }



}

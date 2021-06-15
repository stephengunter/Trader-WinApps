using ApplicationCore.Helpers;
using ApplicationCore.OrderMaker;
using ApplicationCore.OrderMaker.Models;
using ApplicationCore.OrderMaker.Views;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using ApplicationCore.DtoMapper;
using ApplicationCore.OrderMaker.ViewServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using ApplicationCore;
using ApplicationCore.Managers;
using System.Threading;

namespace OrderMaker.Test
{
    public partial class APITestForm : Form
    {
        private IMapper _mapper = MappingConfig.CreateConfiguration().CreateMapper();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private ISettingsManager _settingsManager = Factories.CreateSettingsManager();
        private IOrderMaker _orderMaker;

        private bool _closed = false;

        #region  Helper
        List<TradeSettings> TradeSettings => _settingsManager.TradeSettings;
        TradeSettings FindTradeSettings(string id) => _settingsManager.FindTradeSettings(id);
        #endregion

        List<AccountViewModel> _accounts;
        AccountViewModel Account { get; set; }

        public APITestForm()
        {
            InitializeComponent();

            CreateOrderMaker();
        }
        void CreateOrderMaker()
        {
            string name = _settingsManager.GetSettingValue(AppSettingsKey.OrderMaker);

            _orderMaker = Factories.CreateOrderMaker(name, _settingsManager.BrokageSettings);

            _orderMaker.Ready += OnOrderMakerReady;
            _orderMaker.ConnectionStatusChanged += OnConnectionStatusChanged;
            _orderMaker.ExceptionHappend += OnExceptionHappend;
            _orderMaker.ActionExecuted += OnActionExecuted;

        }
        private void API_TestForm_Load(object sender, EventArgs e)
        {
            _orderMaker.Connect();
        }

        private void OnConnectionStatusChanged(object sender, EventArgs e)
        {
            if (_closed) return;

            try
            {
                var args = e as ConnectionStatusEventArgs;
                _logger.Info($"ConnectionStatusChanged: {args.Status}");


                if (args.Status == ConnectionStatus.DISCONNECTED)
                {
                    _orderMaker.DisConnect();
                    ThreadHelpers.SetTimeout(() =>
                    {
                        _orderMaker.Connect();
                    }, 3000, this);
                }


            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
        }
        private void OnOrderMakerReady(object sender, EventArgs e)
        {
            _logger.Info($"OrderMakerReady. Provider: {_orderMaker.Name}");

            LoadAccounts();
        }
        private void OnActionExecuted(object sender, EventArgs e)
        {
            try
            {
                var args = e as ActionEventArgs;
                _logger.Info($"Action: {args.Action}, Code: {args.Code}, Msg: {args.Msg}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
        private void OnExceptionHappend(object sender, EventArgs e)
        {
            try
            {
                var args = e as ExceptionEventArgs;
                _logger.Error(args.Exception);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
        }


        void LoadAccounts()
        {
            bool error = false;
            var brokerAccounts = _orderMaker.AccountList;
            if (brokerAccounts.IsNullOrEmpty())
            {
                error = true;
                MessageBox.Show($"API查無期貨帳號可下單");
            }
            else
            {
                foreach (var item in TradeSettings)
                {
                    foreach (var acc in item.Accounts)
                    {
                        var exist = brokerAccounts.FirstOrDefault(x => x.Number == acc.Account);
                        if (exist == null)
                        {
                            error = true;
                            MessageBox.Show($"API查無期貨帳號: {acc.Account}");
                        }
                    }
                }
            }

            if (error) return;

            _accounts = new List<AccountViewModel>();
            foreach (var item in TradeSettings)
            {
                foreach (var acc in item.Accounts)
                {
                   _accounts.Add(acc.MapViewModel(item, _mapper));
                }
            }

            this.cbxAccount.Items.Clear();

            foreach (var item in _accounts)
            {
                this.cbxAccount.Items.Add(item.Number);
            }

            this.cbxAccount.SelectedIndex = 0;
        }

        private void cbxAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string accNumber = (string)this.cbxAccount.SelectedItem;

            Account = _accounts.FirstOrDefault(x => x.Number == accNumber);

            GetData();
        }


        void GetData()
        {
            lblSymbol.Text = Account.Symbol;

            if (_orderMaker.Name == BrokageName.HUA_NAN)
            {
                _orderMaker.RequestAccountPositions(Account.Number, Account.Symbol);
            }
            else
            {
                Account.OI = _orderMaker.GetAccountPositions(Account.Number, Account.Symbol);

                Render();
            }

        }

        void Render()
        {

            lblOI.Text = Account.OI.ToString();
            if (Account.OI > 0) lblOI.ForeColor = Color.Red;
            else if (Account.OI == 0) lblOI.ForeColor = Color.Black;
            else lblOI.ForeColor = Color.Green;


            lblSymbol.Text = Account.Symbol;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void OnActionExcuted(object sender, EventArgs e)
        {
            try
            {
                var args = e as ActionEventArgs;
                _logger.Info($"ActionExcuted: {args.Action} , Code: {args.Code} , Msg: {args.Msg}");

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
        }

        private void OrderMaker_AccountPositionUpdated(object sender, EventArgs e)
        {
            try
            {
                var args = e as AccountEventArgs;
                string accountId = args.Account;

                if (Account.Number.EqualTo(args.Account))
                {
                    Account.OI = _orderMaker.GetAccountPositions(Account.Number, Account.Symbol);

                    Render();
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
        }


        void MakeOrder(bool buy)
        {
            var price = txtPrice.Text.ToDecimal();
            if (price <= 0)
            {
                MessageBox.Show("價格錯誤");
                return;
            }

            int lots = buy ? Account.Lots : 0 - Account.Lots;

            int offset = Account.TradeSettings.Offset;

            if (lots > 0) _orderMaker.MakeOrder(Account.Symbol, Account.Number, price + offset, lots, Account.TradeSettings.DayTrade); //買進
            else if (lots < 0) _orderMaker.MakeOrder(Account.Symbol, Account.Number, price - offset, lots, Account.TradeSettings.DayTrade); //賣出

            GetData();
        }

        private void btnBuy_Click(object sender, EventArgs e) => MakeOrder(true);

        private void btnSell_Click(object sender, EventArgs e) => MakeOrder(false);

        private void APITestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closed = true;

            if (_orderMaker != null) _orderMaker.DisConnect();

            Thread.Sleep(1500);
        }
    }
}

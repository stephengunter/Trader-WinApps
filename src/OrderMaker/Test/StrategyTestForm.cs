using ApplicationCore;
using ApplicationCore.Helpers;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using ApplicationCore.OrderMaker.Models;
using ApplicationCore.OrderMaker.Views;
using NLog;
using OrderMaker.Helpers;
using OrderMaker.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderMaker.Test
{
    public partial class StrategyTestForm : Form
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private ISettingsManager _settingsManager = Factories.CreateSettingsManager();
        private ITimeManager _timeManager;
        private IOrderMaker _orderMaker;

        private bool _closed = false;

        #region  Helper
        List<TradeSettings> TradeSettings => _settingsManager.TradeSettings;
        bool HasTradeSettings => TradeSettings.HasItems();
        TradeSettings FindTradeSettings(string id) => _settingsManager.FindTradeSettings(id);
        #endregion

        public StrategyTestForm()
        {
            this._timeManager = Factories.CreateTimeManager(_settingsManager.GetSettingValue(AppSettingsKey.Begin),
                _settingsManager.GetSettingValue(AppSettingsKey.End));

            InitializeComponent();

            if (HasTradeSettings)
            {
                this.tpStrategy.Controls.Add(UIHelpers.CreateLabel("策略設定", Color.Black, DockStyle.Fill), 0, 0);
                CreateOrderMaker();
            }
            else
            {
                this.tpStrategy.Controls.Add(UIHelpers.CreateLabel("您還沒有設定策略. 請先設定策略才可同步下單.", Color.Red, DockStyle.Fill), 0, 0);
            } 

        }

        private void StrategyTestForm_Load(object sender, EventArgs e)
        {
            if (HasTradeSettings)
            {
                _orderMaker.Connect();
            }
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

            InitStrategyUI();

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

        #region  UI
        List<Uc_Strategy> _uc_StrategyList = new List<Uc_Strategy>();
        #endregion

        #region Event Handlers

        private void OnAddStrategy(object sender, EventArgs e)
        {
            this.editStrategy = new EditStrategy();
            this.editStrategy.Init(new TradeSettings());
            this.editStrategy.OnSave += this.OnSaveStrategy;

            this.editStrategy.ShowDialog();
        }

        private void OnEditStrategy(object sender, EventArgs e)
        {
            try
            {
                var args = e as EditStrategyEventArgs;
                var tradeSettings = FindTradeSettings(args.TradeSettings.Id);

                var clone = tradeSettings.DeepCloneByJson();

                this.editStrategy = new EditStrategy();
                this.editStrategy.Init(clone);
                this.editStrategy.OnSave += this.OnSaveStrategy;
                this.editStrategy.OnRemove += this.OnRemoveStrategy;

                this.editStrategy.ShowDialog();

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show("修改策略設定失敗");
            }
        }

        private void OnRemoveStrategy(object sender, EventArgs e)
        {
            try
            {
                var args = e as EditStrategyEventArgs;

                _settingsManager.AddUpdateTradeSettings(args.TradeSettings);

                OnSettinsChanged();

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show("刪除策略失敗");
            }
        }

        private void OnSaveStrategy(object sender, EventArgs e)
        {
            try
            {
                var args = e as EditStrategyEventArgs;
                var tradeSettings = args.TradeSettings;

                _settingsManager.AddUpdateTradeSettings(tradeSettings);

                OnSettinsChanged();

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                MessageBox.Show("儲存策略設定失敗");
            }
        }
        private void OnConfig_Changed(object sender, EventArgs e = null) => OnSettinsChanged();
        #endregion

        void InitStrategyUI()
        {
            if (!HasTradeSettings) return;

            this._uc_StrategyList = new List<Uc_Strategy>();
            this.fpanelStrategies.Height = 1;
            this.fpanelStrategies.Controls.Clear();

            for (int i = 0; i < TradeSettings.Count; i++)
            {
                var uc_Strategy = new Uc_Strategy(_orderMaker, _settingsManager.FindTradeSettings(TradeSettings[i].Id), _timeManager, _logger);
                uc_Strategy.OnEdit += new System.EventHandler(this.OnEditStrategy);


                int height = uc_Strategy.ClientSize.Height;
                this._uc_StrategyList.Add(uc_Strategy);


                fpanelStrategies.Height += height + 3;
                this.fpanelStrategies.Controls.Add(uc_Strategy);
                fpanelStrategies.Controls.SetChildIndex(uc_Strategy, 0);
            }
        }

        void OnSettinsChanged()
        {
            MessageBox.Show("設定檔已變更, 程式將重新啟動.");
            Application.ExitThread();
            ReStart();
        }

        void ReStart()
        {
            Thread thtmp = new Thread(new ParameterizedThreadStart(Run));
            object appName = Application.ExecutablePath;
            Thread.Sleep(3000);
            thtmp.Start(appName);

        }

        private void Run(Object obj)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();

        }

        private void StrategyTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closed = true;

            if (_orderMaker != null) _orderMaker.DisConnect();

            Thread.Sleep(1500);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _orderMaker.DisConnect();
        }
    }
}

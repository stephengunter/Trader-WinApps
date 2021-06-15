using ApplicationCore;
using ApplicationCore.DtoMapper;
using ApplicationCore.Helpers;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using ApplicationCore.OrderMaker.Models;
using AutoMapper;
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

namespace OrderMaker
{
    public partial class Main : Form
    {
        private IMapper _mapper = MappingConfig.CreateConfiguration().CreateMapper();
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private ISettingsManager _settingsManager = Factories.CreateSettingsManager();
        private ITimeManager _timeManager;
        private IOrderMaker _orderMaker;

        private bool _closed = false;

        #region  Helper
        bool _basicSettingOK = false;
        bool CheckBasicSettings() => _basicSettingOK = _settingsManager.CheckBasicSetting();

        List<TradeSettings> TradeSettings => _settingsManager.TradeSettings;
        bool HasTradeSettings => TradeSettings.HasItems();
        TradeSettings FindTradeSettings(string id) => _settingsManager.FindTradeSettings(id);
        #endregion

        #region  UI
        UcStatus ucStatus;
        List<Uc_Strategy> _uc_StrategyList = new List<Uc_Strategy>();
        #endregion

        public Main()
        {
            this._timeManager = Factories.CreateTimeManager(_settingsManager.GetSettingValue(AppSettingsKey.Begin),
                 _settingsManager.GetSettingValue(AppSettingsKey.End));
            
            InitializeComponent();

            CheckBasicSettings();
            if (_basicSettingOK)
            {
                CreateOrderMaker();
            }
            else
            {
                OnEditConfig(null, null);
            }

            InitBasicUI();
            InitStatusUI();

            if (HasTradeSettings)
            {
                this.tpStrategy.Controls.Add(UIHelpers.CreateLabel("策略設定", Color.Black, DockStyle.Fill), 0, 0);               
            }
            else
            {
                this.tpStrategy.Controls.Add(UIHelpers.CreateLabel("您還沒有設定策略. 請先設定策略才可同步下單.", Color.Red, DockStyle.Fill), 0, 0);
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

        #region OrderMaker Event Handlers
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
        private void OnConnectionStatusChanged(object sender, EventArgs e)
        {
            if (_closed) return;

            try
            {
                var args = e as ConnectionStatusEventArgs;
                _logger.Info($"ConnectionStatusChanged: {args.Status}");

                if (ucStatus != null) ucStatus.CheckConnect();

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
        }
        #endregion

        #region Event Handlers
        private void Main_Load(object sender, EventArgs e)
        {
            if (HasTradeSettings)
            {
                _orderMaker.Connect();
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closed = true;

            if (_orderMaker != null) _orderMaker.DisConnect();

            Thread.Sleep(1500);
        }


        private void OnEditConfig(object sender, EventArgs e)
        {
            this.editConfig = new EditConfig(_settingsManager, _timeManager);
            this.editConfig.ConfigChanged += this.OnConfig_Changed;

            this.editConfig.ShowDialog();
        }

        private void OnConfig_Changed(object sender, EventArgs e = null) => OnSettinsChanged();

        private void btnLogs_Click(object sender, EventArgs e)
        {
            var psi = new ProcessStartInfo() { FileName = _settingsManager.LogFilePath, UseShellExecute = true };
            Process.Start(psi);
        }

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

        #endregion



        void InitBasicUI()
        {

            if (_basicSettingOK) this.tpTop.Controls.Add(UIHelpers.CreateLabel("基本設定", Color.Black, DockStyle.Fill), 0, 0);
            else this.tpTop.Controls.Add(UIHelpers.CreateLabel("您還沒有完成基本設定", Color.Red, DockStyle.Fill), 0, 0);

        }
        void InitStatusUI()
        {
            this.ucStatus = new UcStatus(this, _settingsManager, _timeManager, _orderMaker, _logger);
            this.panel1.Controls.Add(this.ucStatus);
        }
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


    }
}

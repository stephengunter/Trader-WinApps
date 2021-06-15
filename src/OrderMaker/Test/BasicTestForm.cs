﻿using ApplicationCore;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
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
    public partial class BasicTestForm : Form
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private ISettingsManager _settingsManager = Factories.CreateSettingsManager();
        private ITimeManager _timeManager;
        private IOrderMaker _orderMaker;

        private bool _closed = false;
        public BasicTestForm()
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

        #region  Helper
        bool _basicSettingOK = false;
        bool CheckBasicSettings() => _basicSettingOK = _settingsManager.CheckBasicSetting();
        #endregion

        #region  UI
        UcStatus ucStatus;
        #endregion

        #region Form Event Handlers
        private void BasicTestForm_Load(object sender, EventArgs e)
        {
            _orderMaker.Connect();

        }
        #endregion

        #region Event Handlers
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
        private void OnOrderMakerReady(object sender, EventArgs e)
        {
            _logger.Info($"OrderMakerReady. Provider: {_orderMaker.Name}");
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

        private void BasicTestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closed = true;

            if (_orderMaker != null) _orderMaker.DisConnect();

            Thread.Sleep(1500);
        }


    }
}

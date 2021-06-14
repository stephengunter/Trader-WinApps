using ApplicationCore;
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
namespace OrderMaker
{
    public partial class Form1 : Form
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private ISettingsManager _settingsManager = Factories.CreateSettingsManager();
        private ITimeManager _timeManager;
        private IOrderMaker _orderMaker;

        private bool _closed = false;
        public Form1()
        {
            _settingsManager = Factories.CreateSettingsManager();

            _timeManager = Factories.CreateTimeManager(
                            _settingsManager.GetSettingValue(AppSettingsKey.Begin),
                            _settingsManager.GetSettingValue(AppSettingsKey.End)
                            );

            CreateOrderMaker();


            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Connect();
        }

        private void Connect() => _orderMaker.Connect();
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
        private void OnConnectionStatusChanged(object sender, EventArgs e)
        {
            try
            {
                var args = e as ConnectionStatusEventArgs;
                _logger.Info($"ConnectionStatusChanged: {args.Status}");

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


        private void button1_Click(object sender, EventArgs e)
        {
            var connected = _orderMaker.Connectted;
            if (connected)
            { 
            
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closed = true;

            if (_orderMaker != null) _orderMaker.DisConnect();

            Thread.Sleep(1500);
        }

        
    }
}

using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using ApplicationCore.OrderMaker.Managers;
using ApplicationCore.OrderMaker.Models;
using ApplicationCore.Helpers;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OrderMaker.Helpers;
using System.Diagnostics;

namespace OrderMaker.UI
{
    public partial class Uc_Strategy : UserControl
    {
        private IOrderMaker _orderMaker;
        private readonly ITimeManager _timeManager;
        private readonly ILogger _logger;

        private TradeSettings _tradeSettings;
        private PositionFile _positionFile;
        private IPositionManager _positionManager;


        #region  UI
        List<Uc_Account> uc_AccountList = new List<Uc_Account>();
        Label lblPosition;
        Label lblTime;
        PositionEdit _positionEditForm;


        void RenderPositionInfo()
        {
            lblPosition.Text = _positionFile.Position.ToString();
            if (_positionFile.Position > 0) lblPosition.ForeColor = Color.Red;
            else if (_positionFile.Position == 0) lblPosition.ForeColor = Color.Black;
            else lblPosition.ForeColor = Color.Green;


            lblTime.Text = $"({_positionFile.Time.ToTimeString()})";
        }
        #endregion


        public Uc_Strategy(IOrderMaker orderMaker, TradeSettings settings, ITimeManager timeManager, ILogger logger)
        {
            this._orderMaker = orderMaker;
            this._tradeSettings = settings;
            this._timeManager = timeManager;
            this._logger = logger;

            InitializeComponent();

            if (!File.Exists(_tradeSettings.FileName)) File.Create(_tradeSettings.FileName).Close();
            _positionFile = new PositionFile();
            _positionManager = Factories.CreatePositionManager(_orderMaker, _tradeSettings, logger);

            this.timer.Interval = _tradeSettings.Interval;
            this.timer.Enabled = true;

            #region  UI
            var lbl = UIHelpers.CreateLabel(_tradeSettings.Name);
            lbl.Font = new System.Drawing.Font("新細明體", 11.25F);
            this.tpTop.Controls.Add(lbl, 0, 0);


            this.tpTop.Controls.Add(UIHelpers.CreateLabel("即時部位："), 2, 0);
            lblPosition = UIHelpers.CreateLabel("");
            this.tpTop.Controls.Add(lblPosition, 3, 0);


            lblTime = UIHelpers.CreateLabel("");
            this.tpTop.Controls.Add(lblTime, 4, 0);


            for (int i = 0; i < _tradeSettings.Accounts.Count; i++)
            {
                var item = _tradeSettings.Accounts[i];

                var uc_Account = new Uc_Account();
                uc_Account.BindData(item);

                this.uc_AccountList.Add(uc_Account);


                fpanelAccounts.Height += uc_Account.Height;
                this.fpanelAccounts.Controls.Add(uc_Account);
                fpanelAccounts.Controls.SetChildIndex(uc_Account, 0);


                this.Height += uc_Account.Height;
            }
            #endregion
        }


        public void Close()
        {
            _positionManager.FetchDeals();
        }

        public event EventHandler OnEdit;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit?.Invoke(this, new EditStrategyEventArgs(_tradeSettings));
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (_timeManager.InTime) OnRound();  //只在盤中進行, 負責部位同步化(實際下單)
        }

        void OnRound()
        {
            try
            {
                var file = ReadPositionFile();
                if (file == null) return;

                _positionManager.SyncPosition(file);

                this._positionFile = file;
                RenderPositionInfo();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        PositionFile ReadPositionFile()
        {
            string text = "";
            using (var sr = new StreamReader(_tradeSettings.FileName)) text = sr.ReadToEnd().Trim();

            if (String.IsNullOrEmpty(text)) return null;

            // 1,9865
            var values = text.Split(',');

            return new PositionFile
            {
                Position = values[0].ToInt(),
                MarketPrice = values[1].ToDecimal(),
                Time = DateTime.Now
            };
        }



        private void btnTest_Click(object sender, EventArgs e)
        {
            bool openFile = true;

            if (openFile) Process.Start(_tradeSettings.FileName);
            else
            {
                int position = 0;
                int price = 0;

                var file = ReadPositionFile();
                if (file != null)
                {
                    position = file.Position;
                    price = Convert.ToInt32(file.MarketPrice);
                }

                _positionEditForm = new PositionEdit(_tradeSettings.Id, position, price);

                _positionEditForm.PositionSubmit += OnPositionEditorSubmit;

                _positionEditForm.ShowDialog();
            }

        }

        private void OnPositionEditorSubmit(object sender, EventArgs e)
        {
            try
            {
                var args = e as PositionEventArgs;
                int position = args.Position;
                int price = args.Price;

                if (args.Id == _tradeSettings.Id)
                {
                    using (var sr = new StreamWriter(_tradeSettings.FileName))
                    {
                        // 1,9865
                        sr.Write($"{position} , {price}");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);

            }
        }
    }
}

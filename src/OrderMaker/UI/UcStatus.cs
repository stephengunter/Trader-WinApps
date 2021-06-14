using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderMaker.UI
{
    public partial class UcStatus : UserControl
    {
		private readonly ISettingsManager _settingsManager;
		private readonly ITimeManager _timeManager;
		private readonly ILogger _logger;
		private IOrderMaker _orderMaker;

		#region  UI
		Label labelOpenTime = new Label();
		Label labelCloseTime = new Label();
		Label labelTime = new Label();
		Label labelStatus = new Label();
		Label labelSource = new Label();
		#endregion

		#region  Helper
		bool InTime => _timeManager.InTime;
		DateTime BeginTime => _timeManager.BeginTime;
		DateTime EndTime => _timeManager.EndTime;
		#endregion


		public UcStatus(ISettingsManager settingsManager, ITimeManager timeManager, IOrderMaker orderMaker, ILogger logger)
		{
			this._settingsManager = settingsManager;
			this._timeManager = timeManager;
			this._orderMaker = orderMaker;
			this._logger = logger;

			InitializeComponent();

			#region  UI
			this.labelOpenTime.Dock = DockStyle.Fill;
			this.tablePanel.Controls.Add(this.labelOpenTime, 0, 0);

			this.labelCloseTime.Dock = DockStyle.Fill;
			this.tablePanel.Controls.Add(this.labelCloseTime, 1, 0);

			this.labelTime.Dock = DockStyle.Fill;
			this.tablePanel.Controls.Add(this.labelTime, 0, 1);

			this.labelStatus.Width = 120;
			this.labelStatus.TextAlign = ContentAlignment.MiddleLeft;
			this.tablePanel.Controls.Add(this.labelStatus, 1, 1);

			this.labelSource.Width = 120;
			this.labelSource.TextAlign = ContentAlignment.MiddleLeft;
			this.tablePanel.Controls.Add(this.labelSource, 0, 2);
			#endregion

			this.timerDisplay.Interval = 1000;  //只負責顯示時間
			this.timerDisplay.Enabled = true;

		}



		void RenderTime()
		{
			labelTime.Text = $"現在：{ DateTime.Now.ToString() }";
			labelOpenTime.Text = $"開始：{ BeginTime.ToString() }";
			labelCloseTime.Text = $"結束：{ EndTime.ToString() }";

			if (InTime)
			{
				labelStatus.BackColor = System.Drawing.Color.Green;
				labelStatus.ForeColor = System.Drawing.Color.White;
				labelStatus.Text = "進行中";
			}
			else
			{
				labelStatus.BackColor = System.Drawing.Color.Gray;
				labelStatus.ForeColor = System.Drawing.Color.White;
				labelStatus.Text = "已結束";
			}

		}

		public void CheckConnect()
		{
			bool connectted = _orderMaker.Connectted;
			RenderConnectted(connectted);


			if (!connectted)
			{
				_orderMaker.DisConnect();
				Thread.Sleep(3000);
				_orderMaker.Connect();
			}

		}
		private void timerDisplay_Tick(object sender, EventArgs e)
		{
			//顯示時間
			RenderTime();

			if (DateTime.Now.Second == 0 && InTime)
			{
				//只在盤中進行,每一分鐘 檢察連線狀態
				CheckConnect();
			}
		}

		public void RenderConnectted(bool connectted)
		{

			if (connectted)
			{
				labelSource.BackColor = System.Drawing.Color.Green;
				labelSource.ForeColor = System.Drawing.Color.White;
				labelSource.Text = "已連線";
			}
			else
			{
				labelSource.BackColor = System.Drawing.Color.Gray;
				labelSource.ForeColor = System.Drawing.Color.White;
				labelSource.Text = "已斷線";

			}

			_logger.Info("OrderMaker Connectted: " + connectted.ToString());
		}
	}
}

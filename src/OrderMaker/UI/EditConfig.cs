using ApplicationCore;
using ApplicationCore.Brokages;
using ApplicationCore.Helpers;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using OrderMaker.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderMaker.UI
{
    public partial class EditConfig : Form
    {
        private readonly ISettingsManager _settingsManager;
        private readonly ITimeManager _timeManager;

        private readonly string defaultPW = "verysecret";
        public EditConfig(ISettingsManager settingsManager, ITimeManager timeManager)
        {
            this._settingsManager = settingsManager;
            this._timeManager = timeManager;

            InitializeComponent();

            #region  UI
            this.tpMain.Dock = DockStyle.Fill;
            this.tpMain.ColumnCount = 2;
            this.tpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            this.tpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));

            this.tpMain.RowCount = 5;
            for (int i = 0; i < this.tpMain.RowCount; i++)
            {
                this.tpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            }

            this.tpMain.Controls.Add(UIHelpers.CreateLabel("開始"), 0, 0);

            openTimePicker.Format = DateTimePickerFormat.Custom;
            openTimePicker.CustomFormat = "HH:mm:ss";

            openTimePicker.ShowUpDown = true;
            openTimePicker.Width = 100;
            this.tpMain.Controls.Add(openTimePicker, 1, 0);



            this.tpMain.Controls.Add(UIHelpers.CreateLabel("結束"), 0, 1);

            closeTimePicker.Format = DateTimePickerFormat.Custom;
            closeTimePicker.CustomFormat = "HH:mm:ss";

            closeTimePicker.ShowUpDown = true;
            closeTimePicker.Width = 100;
            this.tpMain.Controls.Add(closeTimePicker, 1, 1);


            this.tpMain.Controls.Add(UIHelpers.CreateLabel("身分證號"), 0, 2);
            txSID.Width = 100;
            this.tpMain.Controls.Add(txSID, 1, 2);

            this.tpMain.Controls.Add(UIHelpers.CreateLabel("密碼"), 0, 3);
            txPW.Text = this.defaultPW;
            txPW.PasswordChar = '*';
            txPW.Width = 100;
            this.tpMain.Controls.Add(txPW, 1, 3);


            btnSave.Text = "存檔";
            btnSave.Click += new System.EventHandler(this.OnSave);
            this.tpMain.Controls.Add(btnSave, 1, this.tpMain.RowCount - 1);

            this.panel1.Controls.Add(this.tpMain);

            #endregion

            BindData();
        }

        string Begin => AppSettingsKey.Begin;
        string End => AppSettingsKey.End;
        string SID => AppSettingsKey.SID;
        string Password => AppSettingsKey.Password;
        BrokageSettings BrokageSettings => _settingsManager.BrokageSettings;

        #region  UI
        TableLayoutPanel tpMain = new TableLayoutPanel();

        DateTimePicker openTimePicker = new DateTimePicker();
        DateTimePicker closeTimePicker = new DateTimePicker();
        TextBox txSID = new TextBox();
        TextBox txPW = new TextBox();

        Button btnSave = new Button();
        #endregion



        public event EventHandler ConfigChanged;
        void OnConfigChanged(EventArgs e = null)
        {
            ConfigChanged?.Invoke(this, e);
        }

        void BindData()
        {
            openTimePicker.Value = _timeManager.BeginTime;
            closeTimePicker.Value = _timeManager.EndTime;
            txSID.Text = BrokageSettings.SID;
        }

        private void OnSave(object sender, EventArgs e)
        {
            string sid = txSID.Text.Trim().ToUpper();
            string msg = sid.CheckTWSID();
            if (!String.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg);
                return;
            }

            string pw = txPW.Text;
            if (String.IsNullOrEmpty(pw) || pw == this.defaultPW)
            {
                MessageBox.Show("必須填寫密碼");
                return;
            }

            var pairs = new Dictionary<string, string>();
            pairs.Add(Begin, openTimePicker.Value.ToTimeNumber().ToString());
            pairs.Add(End, closeTimePicker.Value.ToTimeNumber().ToString());
            pairs.Add(SID, sid);
            pairs.Add(Password, ApplicationCore.Security.CryptoGraphy.Encrypt(pw, _settingsManager.SecurityKey));


            foreach (var key in pairs.Keys)
            {
                msg = _settingsManager.AddUpdateAppSettings(key, pairs[key]);
                if (!String.IsNullOrEmpty(msg)) break;
            }


            if (String.IsNullOrEmpty(msg)) OnConfigChanged();
            else MessageBox.Show(msg);

        }
    }
}

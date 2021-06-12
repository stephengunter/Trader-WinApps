using ApplicationCore.Helpers;
using ApplicationCore.OrderMaker.Models;
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
    public partial class EditStrategy : Form
    {
        TradeSettings _tradeSettings;

        #region  UI
        Button btnSelectFile;
        List<Uc_AccountEdit> uc_AccountEditList = new List<Uc_AccountEdit>();

        void AddAccountEditUI(AccountSettings accountSettings)
        {
            var uc_AccountEdit = new Uc_AccountEdit();
            uc_AccountEdit.Name = $"accEdit_{accountSettings.Id}";
            uc_AccountEdit.Init(accountSettings);

            uc_AccountEdit.Height = 45;
            this.fpanelAccounts.Height += uc_AccountEdit.Height + 5;
            this.fpanelAccounts.Controls.Add(uc_AccountEdit);
            fpanelAccounts.Controls.SetChildIndex(uc_AccountEdit, 0);

            uc_AccountEdit.OnRemove += new System.EventHandler(this.OnRemoveAccount);
            this.uc_AccountEditList.Add(uc_AccountEdit);
        }
        #endregion
        public EditStrategy()
        {
            InitializeComponent();

            #region  UI
            btnSelectFile = new Button();
            btnSelectFile.Text = "選取";
            btnSelectFile.Size = new Size(45, txtFilePath.ClientSize.Height + 2);
            btnSelectFile.Location = new Point(txtFilePath.ClientSize.Width - btnSelectFile.Width, -1);
            btnSelectFile.Cursor = Cursors.Default;
            btnSelectFile.Click += new System.EventHandler(this.OnSelectFile);

            txtFilePath.Controls.Add(btnSelectFile);
            #endregion


        }

        public event EventHandler OnSave;
        void OnSaveStrategy()
        {
            OnSave?.Invoke(this, new EditStrategyEventArgs(_tradeSettings));

        }

        public event EventHandler OnRemove;
        void OnRemoveStrategy()
        {
            OnRemove?.Invoke(this, new EditStrategyEventArgs(_tradeSettings));
        }


        public void Init(TradeSettings tradeSettings)
        {
            _tradeSettings = tradeSettings;

            if (String.IsNullOrEmpty(_tradeSettings.Id)) this.btnRemove.Hide();
            BindData();
        }

        void BindData()
        {
            txtName.Text = _tradeSettings.Name;
            chkDaytrade.Checked = _tradeSettings.DayTrade;

            numericOffset.Value = _tradeSettings.Offset;

            numericInterval.Value = _tradeSettings.Interval > 0 ? _tradeSettings.Interval : numericInterval.Minimum;


            txtFilePath.Text = _tradeSettings.FileName;

            fpanelAccounts.Controls.Clear();
            this.uc_AccountEditList = new List<Uc_AccountEdit>();

            for (int i = 0; i < this._tradeSettings.Accounts.Count; i++)
            {
                AddAccountEditUI(this._tradeSettings.Accounts[i]);
            }
        }

        private void OnSelectFile(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;
            }
        }





        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            var accountSettings = new AccountSettings();
            _tradeSettings.Accounts.Add(accountSettings);

            AddAccountEditUI(accountSettings);
        }

        private void OnRemoveAccount(object sender, EventArgs e)
        {
            try
            {
                var args = e as RemoveAccountEventArgs;
                var idx = this._tradeSettings.Accounts.FindIndex(x => x.Id == args.Id);
                this._tradeSettings.Accounts.RemoveAt(idx);

                this.uc_AccountEditList.RemoveAt(idx);


                foreach (Control item in fpanelAccounts.Controls)
                {
                    if (item.Name == $"accEdit_{args.Id}")
                    {
                        fpanelAccounts.Controls.Remove(item);
                        break;
                    }
                }

            }
            catch (Exception)
            {

            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("必須填寫策略名稱");
                return;
            }

            if (String.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("必須設定檔案路徑");
                return;
            }

            if (_tradeSettings.Accounts.IsNullOrEmpty())
            {
                MessageBox.Show("必須設定下單帳號");
                return;
            }

            _tradeSettings.Name = txtName.Text.Trim();
            _tradeSettings.DayTrade = chkDaytrade.Checked;
            _tradeSettings.Offset = Convert.ToInt32(numericOffset.Value);
            _tradeSettings.Interval = Convert.ToInt32(numericInterval.Value);
            _tradeSettings.FileName = txtFilePath.Text;


            bool hasError = false;
            foreach (var item in this.uc_AccountEditList)
            {
                try
                {
                    var data = item.GetData();
                    int idx = _tradeSettings.Accounts.FindIndex(x => x.Id == data.Id);
                    _tradeSettings.Accounts[idx] = data;
                }
                catch (Exception ex)
                {
                    hasError = true;
                    MessageBox.Show(ex.Message);
                    break;
                }

            }

            if (!hasError) OnSaveStrategy();

        }


        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否確定刪除?", "刪除策略", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                OnRemoveStrategy();
            }
        }


    }
}

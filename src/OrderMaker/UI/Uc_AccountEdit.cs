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
    public partial class Uc_AccountEdit : UserControl
    {
        AccountSettings _accountSettings;

        public Uc_AccountEdit()
        {
            InitializeComponent();

            this.tpMain.Controls.Add(CreateLabel("帳號"), 0, 0);
            this.tpMain.Controls.Add(CreateLabel("商品"), 2, 0);
            this.tpMain.Controls.Add(CreateLabel("口數"), 4, 0);


        }

        public event EventHandler OnRemove;
        void OnRemoveAccount()
        {
            OnRemove?.Invoke(this, new RemoveAccountEventArgs(_accountSettings.Id));
        }

        public void Init(AccountSettings accountSettings)
        {
            _accountSettings = accountSettings;
            BindData();
        }

        public AccountSettings GetData()
        {
            if (String.IsNullOrEmpty(this.txtAccount.Text)) throw new Exception("必須填寫帳號");

            _accountSettings.Account = this.txtAccount.Text;
            _accountSettings.Symbol = this.cbSymbol.SelectedItem.ToString();
            _accountSettings.Lot = Convert.ToInt32(this.numericLots.Value);
            return _accountSettings;
        }

        void BindData()
        {
            this.txtAccount.Text = _accountSettings.Account;
            LoadSymbols(_accountSettings.Symbol);
            this.numericLots.Value = _accountSettings.Lot > 0 ? _accountSettings.Lot : 1;
        }

        void LoadSymbols(string symbol)
        {
            this.cbSymbol.Items.Clear();

            this.cbSymbol.Items.Add("TX");
            this.cbSymbol.Items.Add("MTX");

            this.cbSymbol.SelectedIndex = symbol == "MTX" ? 1 : 0;
        }

        Label CreateLabel(string text)
        {
            var lbl = new Label();
            lbl.TextAlign = ContentAlignment.MiddleRight;
            lbl.Text = text;
            return lbl;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            OnRemoveAccount();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderMaker.Models;
using System.Windows.Forms;
using ApplicationCore.Helpers;
using ApplicationCore.OrderMaker;

namespace OrderMaker.UI
{
    public partial class PositionEdit : Form
    {
        private readonly string _id;

        int _position = 0;
        int _price = 0;

        public PositionEdit(string id, int position, int price)
        {
            _id = id;
            _position = position;
            _price = price;

            InitializeComponent();

            this.cbxPosition.Items.Clear();
            for (int i = -1; i <= 1; i++)
            {
                this.cbxPosition.Items.Add(new ComboboxItem { Text = i.ToString(), Value = i.ToString() });
                if (i == position) this.cbxPosition.SelectedIndex = i + 1;
            }

            this.txtPrice.Text = price > 0 ? price.ToString() : "";

        }

        public event EventHandler PositionSubmit;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var price = txtPrice.Text.ToInt();
            if (price <= 0)
            {
                MessageBox.Show("價格錯誤");
                return;
            }

            PositionSubmit?.Invoke(this, new PositionEventArgs(_id, _position, price));

            this.Close();
        }

        private void cbxPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = this.cbxPosition.SelectedItem as ComboboxItem;

            this._position = item.Value.ToInt();
        }
    }
}

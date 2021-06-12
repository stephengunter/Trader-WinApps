using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker.Views
{
    public class DealViewModel
    {
		public string AccountId { get; set; }
		public string Date { get; set; }
		public string Time { get; set; }
		public string ProductId { get; set; }
		public string BS { get; set; }
		public int Price { get; set; }
		public int Qty { get; set; }
	}
}

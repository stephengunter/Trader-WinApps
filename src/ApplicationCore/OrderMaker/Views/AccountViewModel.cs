using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker.Views
{
	public class AccountViewModel
	{
		public List<PositionViewModel> Positions { get; set; } = new List<PositionViewModel>();
		public List<DealViewModel> Deals { get; set; } = new List<DealViewModel>();

		public string Number { get; set; }
		public string Symbol { get; set; }
		public int Lots { get; set; }

		public int OI { get; set; }

		public TradeSettingsViewModel TradeSettings { get; set; }

		public IEnumerable<string> Text { get; set; }

	}



}

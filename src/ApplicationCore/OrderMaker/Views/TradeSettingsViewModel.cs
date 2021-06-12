using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker.Views
{
	public class TradeSettingsViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public int Interval { get; set; }

		public bool DayTrade { get; set; }

		public string FileName { get; set; }

		public int Offset { get; set; }

	}
}

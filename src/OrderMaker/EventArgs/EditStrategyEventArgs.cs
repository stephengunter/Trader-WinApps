using ApplicationCore.OrderMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMaker
{
	public class EditStrategyEventArgs : EventArgs
	{
		public EditStrategyEventArgs(TradeSettings tradeSettings)
		{
			this.TradeSettings = tradeSettings;
		}

		public TradeSettings TradeSettings { get; private set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker.Models
{
	public class AccountSettings
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public TradeSettings TradeSettings { get; set; }
		public string Account { get; set; }
		public string Symbol { get; set; }
		//倍數
		public int Lot { get; set; }
	}
}

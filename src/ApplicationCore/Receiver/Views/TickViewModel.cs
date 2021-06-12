using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Receiver.Views
{
	public class TickViewModel
	{
		public int Order { get; set; }
		public int Time { get; set; }

		public double Price { get; set; }
		public double Bid { get; set; }
		public double Offer { get; set; }

		public int Qty { get; set; }

		public int Type
		{
			get
			{
				if (Price == Bid) return -1;
				else if (Price == Offer) return 1;
				return 0;
			}
		}

	}
}

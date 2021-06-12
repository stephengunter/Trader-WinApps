using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Receiver.Views
{
	public class QuoteViewModel
	{
		public int Date { get; set; }

		public int Time { get; set; }

		public decimal Price { get; set; }

		public decimal Open { get; set; }

		public decimal High { get; set; }

		public decimal Low { get; set; }

		public decimal Vol { get; set; }

	}
}

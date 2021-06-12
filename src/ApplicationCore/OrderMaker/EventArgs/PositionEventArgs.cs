using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker
{
	public class PositionEventArgs : EventArgs
	{
		public PositionEventArgs(string id, int position, int price)
		{
			this.Id = id;
			this.Position = position;
			this.Price = price;
		}

		public string Id { get; private set; }
		public int Position { get; private set; }
		public int Price { get; private set; }
	}
}

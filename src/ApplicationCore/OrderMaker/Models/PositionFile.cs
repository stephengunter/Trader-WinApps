using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker.Models
{
	public interface IPositionInfo
	{
		int Position { get; set; }
		decimal MarketPrice { get; set; }
	}
	public class PositionFile : IPositionInfo
	{
		public int Position { get; set; }
		public decimal MarketPrice { get; set; }
		public DateTime Time { get; set; }

	}
}

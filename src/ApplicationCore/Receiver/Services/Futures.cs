using ApplicationCore.Helpers;
using ApplicationCore.Receiver.Views;
using ApplicationCore.Receiver.ViewServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Receiver.Services
{
	public interface IFuturesLocalService
	{
		void SaveTick(TickViewModel tick);
		IEnumerable<TickViewModel> GetTicks(int begin, int end);
		IEnumerable<TickViewModel> GetTicks(int end);

		IEnumerable<TickViewModel> GetAllTicks();
		QuoteViewModel GetQuote(int begin, int end);
	}

	public class FuturesLocalService : IFuturesLocalService
	{
		private const string TX_CODE = "TX";

		List<TickViewModel> _ticks = new List<TickViewModel>();

		public void SaveTick(TickViewModel tick)
		{
			var exist = _ticks.FirstOrDefault(t => t.Order == tick.Order);
			if (exist == null) _ticks.Add(tick);
		}

		public IEnumerable<TickViewModel> GetTicks(int begin, int end) => _ticks.Where(t => t.Time >= begin && t.Time < end).GetOrdered();
		public IEnumerable<TickViewModel> GetTicks(int end) => _ticks.Where(t => t.Time < end).GetOrdered();
		public IEnumerable<TickViewModel> GetAllTicks() => _ticks.GetOrdered();
		public QuoteViewModel GetQuote(int begin, int end)
		{
			var tickList = GetTicks(begin, end);

			if (tickList.IsNullOrEmpty()) return null;

			return new QuoteViewModel
			{
				Time = end,
				High = tickList.Max(t => (int)t.Price),
				Low = tickList.Min(t => (int)t.Price),
				Open = (int)tickList.First().Price,
				Price = (int)tickList.Last().Price
			};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Receiver
{
	public interface IQuoteSource
	{
		BrokageName Name { get; }
		bool Connectted { get; }
		void Connect();
		void DisConnect();


		void RequestQuotes(IEnumerable<string> symbolCodes);

		event EventHandler ExceptionHappend;

		event EventHandler Ready;

		event EventHandler ConnectionStatusChanged;

		event EventHandler ActionExecuted;

		event EventHandler NotifyTick;
	}
}

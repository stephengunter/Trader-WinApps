using ApplicationCore.Receiver.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Receiver
{
	public class TickEventArgs : EventArgs
	{
		public TickEventArgs(string code, TickViewModel tick, bool realTime)
		{
			this.Code = code;
			this.Tick = tick;
			this.RealTime = realTime;
		}

		public string Code { get; private set; }
		public TickViewModel Tick { get; private set; }
		public bool RealTime { get; private set; }

	}
}

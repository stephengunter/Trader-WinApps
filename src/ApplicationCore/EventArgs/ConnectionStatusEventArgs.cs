using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore
{
	public class ConnectionStatusEventArgs : EventArgs
	{
		public ConnectionStatusEventArgs(ConnectionStatus status)
		{
			this.Status = status;
		}


		public ConnectionStatus Status { get; private set; }
	}
}

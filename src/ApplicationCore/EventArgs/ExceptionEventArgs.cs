using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore
{
	public class ExceptionEventArgs : EventArgs
	{
		public ExceptionEventArgs(Exception exception)
		{
			this.Exception = exception;
		}


		public Exception Exception { get; private set; }

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore
{
	public class ActionEventArgs : EventArgs
	{
		public ActionEventArgs(string action, string code, string msg)
		{
			this.Msg = msg;
			this.Action = action;
			this.Code = code;
		}


		public string Action { get; private set; }
		public string Code { get; private set; }
		public string Msg { get; private set; }
	}
}

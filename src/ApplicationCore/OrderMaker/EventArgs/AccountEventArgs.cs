using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker
{
	public class AccountEventArgs : EventArgs
	{
		public AccountEventArgs(string account)
		{
			this.Account = account;
		}


		public string Account { get; private set; }
	}
}

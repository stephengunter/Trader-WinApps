using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using ApplicationCore.Receiver;
using ApplicationCore.Receiver.Views;
using SKCOMLib;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Brokages.Concord
{
    public partial class ConcordBrokage
    {

        public event EventHandler NotifyTick;
        public void RequestQuotes(IEnumerable<string> symbolCodes)
        {
            OnActionExecuted("RequestQuotes");
        }
    }
}

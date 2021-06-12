using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using ApplicationCore.Receiver;
using ApplicationCore.Receiver.Views;
using SKCOMLib;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Brokages.Fake
{
    public partial class FakeBrokage
    {

        public event EventHandler NotifyTick;
        public void RequestQuotes(IEnumerable<string> symbolCodes)
        {
            OnActionExecuted("RequestQuotes");
        }
    }
}

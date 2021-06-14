using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using ApplicationCore.OrderMaker.Views;
using ApplicationCore.Receiver;
using ApplicationCore.Receiver.Views;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Brokages.Fake
{
    public partial class FakeBrokage : BaseOrderMaker, IQuoteSource
    {
        bool _connected = false;
        public FakeBrokage(BrokageSettings settings) : base(BrokageName.FAKE, settings)
        {
            
        }
        public void Initialize()
        {
            
        }
        public override void Connect()
        {
            _connected = true;
        }

        public override void DisConnect()
        {
            _connected = false;
        }
        


        void OnActionExecuted(string action, string code = "", string msg = "")
           => OnActionExecuted(new ActionEventArgs(action, code, msg));
    }
}

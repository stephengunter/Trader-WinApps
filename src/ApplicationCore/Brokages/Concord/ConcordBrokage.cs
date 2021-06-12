using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using ApplicationCore.OrderMaker.Views;
using ApplicationCore.Receiver;
using ApplicationCore.Receiver.Views;
using System;
using System.Collections.Generic;
using Concord.API.Future.Client;

namespace ApplicationCore.Brokages.Concord
{
    public partial class ConcordBrokage : BaseOrderMaker, IQuoteSource
    {
        private ucClient _API = new ucClient();
        public ConcordBrokage(BrokageSettings settings) : base(BrokageName.CONCORD, settings)
        {
            
        }
        string SID => BrokageSettings.SID.ToUpper();
        string Password => BrokageSettings.Password;
        string IP => BrokageSettings.IP;

        public override void Connect()
        {
            
        }

        public override void DisConnect()
        {
            
        }

        void Login()
        {
            SetConnectionStatus(ConnectionStatus.CONNECTING);
            
            //string result = _API.Login(SID, Password, IP, out _strMsg);

            //if (result == LOGIN_SUCCESS)
            //{
            //    SetConnectionStatus(ConnectionStatus.CONNECTED);
            //    SetReady(true);
            //}

            //OnActionExecuted(LOG_IN, result);

        }

        void OnActionExecuted(string action, string code = "", string msg = "")
           => OnActionExecuted(new ActionEventArgs(action, code, msg));
    }
}

using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker;
using ApplicationCore.Receiver;
using ApplicationCore.Receiver.Views;
using SKCOMLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Brokages.Capital
{
    public partial class CapitalBrokage : BaseOrderMaker, IQuoteSource
    {
        private SKCenterLib _SKCenterLib;
        private SKReplyLib _SKReplyLib;
        public CapitalBrokage(BrokageSettings settings) : base(BrokageName.CAPITAL, settings)
        {
            _SKCenterLib = new SKCenterLib();
            _SKCenterLib.SKCenterLib_SetAuthority(1);
            if (!String.IsNullOrEmpty(settings.LogFile)) _SKCenterLib.SKCenterLib_SetLogPath(settings.LogFile);

            _SKReplyLib = new SKReplyLib();
            _SKReplyLib.OnReplyMessage += OnReplyMessage;

            InitReceiver();

            InitOrderMaker();

            Login();
        }

        string SID => BrokageSettings.SID.ToUpper();
        string Password => BrokageSettings.Password;

        public override bool Connectted
        {
            get
            {
               int code = _SKQuoteLib.SKQuoteLib_IsConnected();
               return code == 1;
            }
        }


        public override void Connect()
        {
            if (Login())
            {
                ConnectQuoteSource();
                InitializeOrderMaker();
            }
        }

        public override void DisConnect()
        {
            int code = _SKQuoteLib.SKQuoteLib_LeaveMonitor();
            OnActionExecuted("SKQuoteLib_LeaveMonitor", code);

        }
        public bool Login()
        {
            int code = _SKCenterLib.SKCenterLib_LoginSetQuote(SID, Password, "Y");

            if (code == 0 || code == 2003) //2003: SK_WARNING_LOGIN_ALREADY
            {
                return true;
            }
            else
            {
                string msg = GetReturnCodeMessage(code);
                throw new Exception($"Login Failed. Receiver: {Name} , Code: {code} , Msg: {msg}");
            }
        }

        void OnReplyMessage(string strUserID, string bstrMessage, out short nConfirmCode)
        {
            OnActionExecuted(new ActionEventArgs("OnReplyMessage", "", bstrMessage));
            nConfirmCode = -1;
        }
        void OnExceptionHappend(string action, int code, string msg = "")
        {
            if (String.IsNullOrEmpty(msg)) msg = GetReturnCodeMessage(code);

            OnExceptionHappend(new QuoteSourceException($"Action: {action}, Code: {code}, Msg: {msg}"));
        }

        void OnActionExecuted(string action, int code)
        {
            string msg = GetReturnCodeMessage(code);

            OnActionExecuted(new ActionEventArgs(action, code.ToString(), msg));
        }

        string GetReturnCodeMessage(int code) => _SKCenterLib.SKCenterLib_GetReturnCodeMessage(code);


    }
}

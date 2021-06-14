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
    public partial class ConcordBrokage : BaseOrderMaker
    {
        private ucClient _API = new ucClient();
        #region Consts
        private const string LOGIN_BEGIN = "101";  //登入開始
        private const string LOGIN_SUCCESS = "102";  //登入OK
        private const string LOGOUT_SUCCESS = "103";  //登出成功

        private const string ORDER_CONNECT_SUCCESS = "110";  //下單連線成功
        private const string ORDER_LOGIN_SUCCESS = "111";  //下單登入驗證成功
        private const string REPLY_CONNECT_SUCCESS = "114";  //回報連線成功
        private const string REPLY_LOGIN_SUCCESS = "115";  //回報登入驗證成功
        private const string REPLY_REGISTER_SUCCESS = "116";  //回報註冊完成
        private const string ORDER_CONNECT_END = "113";  //下單連線結束
        private const string REPLY_CONNECT_END = "118";  //回報連線結束

        private const string ACCESS_FAIL = "201"; //登入主機無法連線
        private const string NOT_LOGIN = "202";  //尚未登入
        private const string CONNECTION_LOST = "210";  //下單連線中斷
        private const string LOGIN_FAILED = "211";  //下單登入驗證失敗
        #endregion
        public ConcordBrokage(BrokageSettings settings) : base(BrokageName.CONCORD, settings)
        {
            _API.OnFGeneralReport += new ucClient.dlgFGeneralReport(API_OnFGeneralReport);
            _API.OnFErrorReport += new ucClient.dlgFErrorReport(API_OnFErrorReport);
            _API.OnFOrderReport += new ucClient.dlgFOrderReport(API_OnFOrderReport);
        }

        #region Properties
        string SID => BrokageSettings.SID.ToUpper();
        string Password => BrokageSettings.Password;
        string IP => BrokageSettings.IP;
        #endregion

        
        public override bool Connectted
        {
            get
            {
                string code = _API.FCheckConnect(out string msg);
                OnActionExecuted("CheckConnect", code, msg);
                return code == LOGIN_SUCCESS;
            }
        }

        public override void Connect() => Login();

        public override void DisConnect() => Logout();

        void Login()
        {
            SetConnectionStatus(ConnectionStatus.CONNECTING);

            string code = _API.Login(SID, Password, IP, out string msg);
            OnActionExecuted("Login", code, msg);
        }

        void Logout()
        {
            string code = _API.Logout(out string msg);
            OnActionExecuted("Logout", code, msg);
        }
        void Initialize()
        {
            //檢查憑證
            CheckCertStatus();

            LoadAccounts();

            SetReady(true);
        }

        void OnActionExecuted(string action, string code, string msg)
        {
            if (code == LOGIN_SUCCESS)
            {
                SetConnectionStatus(ConnectionStatus.CONNECTED);
                Initialize();
                
            }
            else if (code == ACCESS_FAIL || code == NOT_LOGIN) // 201 登入主機無法連線 202 尚未登入
            {
                SetConnectionStatus(ConnectionStatus.DISCONNECTED);
            }
            else if (code == CONNECTION_LOST || code == LOGIN_FAILED) // 下單連線中斷 , 登入驗證失敗
            {
                SetConnectionStatus(ConnectionStatus.DISCONNECTED);
            }

            OnActionExecuted(new ActionEventArgs(action, code, msg));
        }

        void OnExceptionHappend(string action, string code, string msg)
        {
            OnExceptionHappend(new OrderMakerException($"Action: {action}, Code: {code}, Msg: {msg}"));
        }

        #region APIEvents
        private void API_OnFGeneralReport(string strMsgCode, string strMsg)
        {
            OnActionExecuted("OnFGeneralReport", strMsgCode, strMsg);
        }
        private void API_OnFErrorReport(string strMsgCode, string strMsg)
        {
            OnActionExecuted(new ActionEventArgs("FErrorReport", strMsgCode, strMsg));
        }
        private void API_OnFOrderReport(string strMsgCode, string strMsg)
        {
            OnActionExecuted(new ActionEventArgs("FOrderRepor", strMsgCode, strMsg));
        }
        #endregion
    }
}

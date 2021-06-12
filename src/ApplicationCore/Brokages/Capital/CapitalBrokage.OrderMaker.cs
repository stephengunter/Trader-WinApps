using ApplicationCore.Exceptions;
using ApplicationCore.Helpers;
using ApplicationCore.Managers;
using ApplicationCore.OrderMaker.Views;
using ApplicationCore.Receiver.Views;
using SKCOMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Brokages.Capital
{
    public partial class CapitalBrokage
    {
        private SKOrderLib _SKOrderLib;
        void InitOrderMaker()
        {
            _SKOrderLib = new SKOrderLib();
            _SKOrderLib.OnAccount += new _ISKOrderLibEvents_OnAccountEventHandler(SKOrderLib_OnAccount);

        }

        
        string _OrderLibInitializeErrors = "";
        void InitializeOrderMaker()
        {
            _OrderLibInitializeErrors = "";

            int code = _SKOrderLib.SKOrderLib_Initialize();
            if (code == 0)
            {
                OnActionExecuted("SKOrderLib_Initialize", code);

                string errMsg = ReadCert(SID);
                if (!String.IsNullOrEmpty(errMsg)) _OrderLibInitializeErrors += errMsg;

                errMsg = GetUserAccounts();
                if (!String.IsNullOrEmpty(errMsg)) _OrderLibInitializeErrors += errMsg;
            }
            else
            {
                string msg = GetReturnCodeMessage(code);
                _OrderLibInitializeErrors = msg;
                OnExceptionHappend("SKOrderLib_Initialize", code, msg);
            }
        }

        string ReadCert(string sid)
        {
            int code = _SKOrderLib.ReadCertByID(sid);
            if (code == 0 || code == 2005)  //SK_WARNING_CERT_VERIFIED_ALREADY
            {
                OnActionExecuted("ReadCert", code);
                return "";
            }
            else
            {
                string msg = GetReturnCodeMessage(code);
                OnExceptionHappend("ReadCert", code, msg);

                return msg;
            }
        }

        string GetUserAccounts()
        {
            int code = _SKOrderLib.GetUserAccount();
            if (code == 0)
            {
                OnActionExecuted("GetUserAccounts", code);
                return "";
            }
            else
            {
                string msg = GetReturnCodeMessage(code);
                OnExceptionHappend("GetUserAccounts", code, msg);

                return msg;
            }
        }

        void SKOrderLib_OnAccount(string bstrLogInID, string bstrAccountData)
        {
            string[] strValues = bstrAccountData.Split(',');
            var accountModel = new AccountViewModel
            {
                 Number = strValues[3],
                 Text = strValues
            };

            AddAccount(accountModel);
        }

        public override string ClearOrders(string symbol, string account)
        {
            throw new NotImplementedException();
        }
       

        public override void FetchDeals(string account)
        {
            throw new NotImplementedException();
        }

        public override List<DealViewModel> GetDeals()
        {
            throw new NotImplementedException();
        }

        Dictionary<string, string> _accountInfoes = new Dictionary<string, string>();
        string GetFullAccountId(string accNum)
        {
            if (_accountInfoes.ContainsKey(accNum)) return _accountInfoes[accNum];
            var account = FindAccountByNumber(accNum);
            if (account == null) return "";

            string fullAccountId = account.Text.ResolveFullAccountId();
            _accountInfoes.Add(accNum, fullAccountId);

            return fullAccountId;
        }
        public override void MakeOrder(string symbol, string account, decimal price, int lots, bool dayTrade)
        {
            

            bool test = (symbol == "TXO16000R1");
            test = (account == "9564156");
            test = (price == 3);
            test = (lots == 1);

            string fullAccountId = GetFullAccountId(account);
            string branch = "F020000";
            test = (fullAccountId == $"{branch}{account}");

            if (!test) throw new Exception();

            string stockNo = "TXO16000R1";

            var pFutureOrder = new FUTUREORDER()
            {
                bstrFullAccount = fullAccountId,
                bstrStockNo = stockNo,
                nQty = lots,
                bstrPrice = price.ToString(),
                sBuySell = 0,
                sNewClose = 2
            };

            string bstrLogInID = SID;
            bool bAsyncOrder = false;

            string strMessage = "";
            int m_nCode = _SKOrderLib.SendOptionOrder(bstrLogInID, bAsyncOrder, pFutureOrder, out strMessage);
        }

        public override void RequestAccountPositions(string account, string symbol = "")
        {

        }

    }
}

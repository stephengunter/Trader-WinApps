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

namespace ApplicationCore.Brokages.Concord
{
    public partial class ConcordBrokage
    {
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

        public override void MakeOrder(string symbol, string account, decimal price, int lots, bool dayTrade)
        {
            string bs = lots > 0 ? "B" : "S";
            string strPrice = price > 0 ? Convert.ToInt32(price).ToString() : "0";
            string qty = Math.Abs(lots).ToString();

            OnActionExecuted($"MakeOrder: {bs} {qty} {symbol} , price: {strPrice} ");
        }

        public override void RequestAccountPositions(string account, string symbol = "")
        {
            OnActionExecuted("RequestAccountPositions");
        }

    }
}

using ApplicationCore.OrderMaker.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.OrderMaker
{
    public interface IOrderMaker
    {

        event EventHandler ExceptionHappend;

        event EventHandler Ready;

        event EventHandler ConnectionStatusChanged;

        event EventHandler ActionExecuted;

        event EventHandler AccountPositionUpdated;


        BrokageName Name { get; }

        List<AccountViewModel> AccountList { get; }
        ConnectionStatus ConnectionStatus { get; }
        bool Connectted { get; }

        void Connect();

        void DisConnect();
        void RequestAccountPositions(string account, string symbol = "");
        int GetAccountPositions(string account, string symbol);

        void MakeOrder(string symbol, string account, decimal price, int lots, bool dayTrade);

        string ClearOrders(string symbol, string account);

        void FetchDeals(string account);

        List<DealViewModel> GetDeals();
    }
}

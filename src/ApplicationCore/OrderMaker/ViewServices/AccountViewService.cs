
using ApplicationCore.OrderMaker.Models;
using ApplicationCore.OrderMaker.Views;
using AutoMapper;

namespace ApplicationCore.OrderMaker.ViewServices
{
    public static class AccountViewService
    {
        public static AccountViewModel MapViewModel(this AccountSettings account, TradeSettings tradeSettings, IMapper mapper)
        {
            return new AccountViewModel()
            {
                TradeSettings = tradeSettings.MapViewModel(mapper),
                Number = account.Account,
                Symbol = account.Symbol,
                Lots = account.Lot
            };
        }
    }
}

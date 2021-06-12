
using ApplicationCore.OrderMaker.Models;
using ApplicationCore.OrderMaker.Views;
using AutoMapper;


namespace ApplicationCore.OrderMaker.ViewServices
{
    public static class TradeSettingsViewService
    {
        public static TradeSettingsViewModel MapViewModel(this TradeSettings settings, IMapper mapper)
            => mapper.Map<TradeSettingsViewModel>(settings);
    }
}

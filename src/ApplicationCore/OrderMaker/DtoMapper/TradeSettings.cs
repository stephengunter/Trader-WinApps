using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.OrderMaker.Models;
using ApplicationCore.OrderMaker.Views;
using AutoMapper;

namespace ApplicationCore.OrderMaker.DtoMapper
{
	public class TradeSettingsMappingProfile : Profile
	{
		public TradeSettingsMappingProfile()
		{
			CreateMap<TradeSettings, TradeSettingsViewModel>();

			CreateMap<TradeSettingsViewModel, TradeSettings>();
		}
	}
}

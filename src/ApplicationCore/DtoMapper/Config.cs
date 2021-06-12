using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationCore.OrderMaker.DtoMapper;

namespace ApplicationCore.DtoMapper
{
	public class MappingConfig
	{
		public static MapperConfiguration CreateConfiguration()
		{
			return new MapperConfiguration(cfg => {
				cfg.AddMaps(typeof(TradeSettingsMappingProfile).Assembly);
			});
		}

	}

	
}

using AutoMapper;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.StoreDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Mappings
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<StoresDto, Store>();
            CreateMap<StoresDto, Store>().ReverseMap();
            CreateMap<StoreAddDto, Store>();
            CreateMap<StoreAddDto, Store>().ReverseMap();
            CreateMap<StoreUpdateDto, Store>();
            CreateMap<StoreUpdateDto, Store>().ReverseMap();
        }
    }
}

using AutoMapper;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrdersDto, Order>();
            CreateMap<OrdersDto, Order>().ReverseMap();
            CreateMap<OrderAddDto, Order>();
            CreateMap<OrderAddDto, Order>().ReverseMap();
            CreateMap<OrderUpdateDto, Order>();
            CreateMap<OrderUpdateDto, Order>().ReverseMap();
        }
    }
}
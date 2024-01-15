using AutoMapper;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductsDto, Product>();
            CreateMap<ProductsDto, Product>().ReverseMap();
            CreateMap<ProductAddDto, Product>();
            CreateMap<ProductAddDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
        }
    }
}

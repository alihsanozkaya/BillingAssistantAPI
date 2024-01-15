using AutoMapper;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoriesDto, Category>();
            CreateMap<CategoriesDto, Category>().ReverseMap();
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryAddDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        }
    }
}

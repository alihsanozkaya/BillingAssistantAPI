using AutoMapper;
using BillingAssistant.Core.Entities.Concrete.Auth;
using BillingAssistant.Entities.DTOs.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Mappings
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();    
        }
    }
}

using BillingAssistant.Core.Entities.Concrete.Auth;
using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.Core.Utilities.Security.JWT;
using BillingAssistant.Entities.Concrete;
using BillingAssistant.Entities.DTOs.AuthDtos;
using BillingAssistant.Entities.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        Task<IDataResult<UserForUpdateProfileDto>> UpdateProfile(UserForUpdateProfileDto userForUpdateProfileDto);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        Task<IDataResult<UserForVerifiedDto>> Verification(UserForVerifiedDto userForVerifiedDto);
        Task<IDataResult<UserForVerifiedDto>> GetUserVerificationStatus(string userEmail);

        Task<IDataResult<UserDto>> GetUserProfile(int id);
             
    }
}
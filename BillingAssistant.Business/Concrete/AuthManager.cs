using AutoMapper;
using BillingAssistant.Business.Abstract;
using BillingAssistant.Business.Constants;
using BillingAssistant.Core.Entities.Concrete.Auth;
using BillingAssistant.Core.Utilities.Results;
using BillingAssistant.Core.Utilities.Security.Hashing;
using BillingAssistant.Core.Utilities.Security.JWT;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.Entities.DTOs.AuthDtos;
using BillingAssistant.Entities.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private IUserRepository _userRepository;
        private ITokenHelper _tokenHelper;
        private IMapper _mapper;
        public AuthManager(IUserService userService, IUserRepository userRepository, ITokenHelper tokenHelper, IMapper mapper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userRepository = userRepository;
            _mapper = mapper;
        }     
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }
        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task<IDataResult<UserForVerifiedDto>> Verification(UserForVerifiedDto? userForVerifiedDto)
        {
            if (userForVerifiedDto == null)
            {
                return new ErrorDataResult<UserForVerifiedDto>(null, Messages.InvalidData);
            }

            var userToCheck = await _userRepository.GetAsync(x => x.Email == userForVerifiedDto.Email);

            if (userToCheck == null)
            {
                return new ErrorDataResult<UserForVerifiedDto>(null, Messages.UserNotFound);
            }

            if (userToCheck.Verified)
            {
                return new ErrorDataResult<UserForVerifiedDto>(null, Messages.UserAlreadyVerified);
            }

            if (!userToCheck.Verified && userForVerifiedDto.Verified)
            {
                //userToCheck.FirstName = userForVerifiedDto.FirstName;
                //userToCheck.LastName = userForVerifiedDto.LastName;
                userToCheck.Verified = userForVerifiedDto.Verified;

                var userVerificationUpdate = await _userRepository.UpdateAsync(userToCheck);

                var resultUpdateDto = new UserForVerifiedDto
                {
                    //FirstName = userToCheck.FirstName,
                    //LastName = userToCheck.LastName,
                    Email = userToCheck.Email,
                    Verified = userToCheck.Verified
                };

                return new SuccessDataResult<UserForVerifiedDto>(resultUpdateDto, Messages.VerificationDone);
            }

            return new ErrorDataResult<UserForVerifiedDto>(null, Messages.VerificationFailed);
        }


    }
}
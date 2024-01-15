
using AutoMapper;
using BillingAssistant.Business.Abstract;
using BillingAssistant.Core.Entities.Concrete.Auth;
using BillingAssistant.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;  
        }
        public List<OperationClaim> GetClaims(User user)
        {
            return _userRepository.GetClaims(user);
        }
        public void Add(User user)
        { 
            _userRepository.AddAsync(user);
        }
        public User GetByMail(string email)
        {
            return _userRepository.Get(u => u.Email == email);
        }
    }
}

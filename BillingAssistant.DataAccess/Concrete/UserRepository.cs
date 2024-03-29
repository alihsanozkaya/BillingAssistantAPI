﻿using BillingAssistant.Core.DataAccess.EntityFramework;
using BillingAssistant.Core.Entities.Concrete.Auth;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.DataAccess.Concrete.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.DataAccess.Concrete
{
    public class UserRepository : EfBaseRepository<User, ApplicationDbContext>, IUserRepository
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}
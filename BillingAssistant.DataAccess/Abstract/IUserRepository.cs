using BillingAssistant.Core.DataAccess;
using BillingAssistant.Core.Entities.Concrete.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.DataAccess.Abstract
{
    public interface IUserRepository : IBaseRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
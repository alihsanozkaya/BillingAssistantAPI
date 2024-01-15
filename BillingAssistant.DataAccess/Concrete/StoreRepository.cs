using BillingAssistant.Core.DataAccess.EntityFramework;
using BillingAssistant.DataAccess.Abstract;
using BillingAssistant.DataAccess.Concrete.Contexts;
using BillingAssistant.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.DataAccess.Concrete
{
    public class StoreRepository: EfBaseRepository<Store, ApplicationDbContext>, IStoreRepository
    {
    }
}

using BillingAssistant.Core.DataAccess;
using BillingAssistant.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.DataAccess.Abstract
{
    public interface IProductRepository : IBaseRepository<Product>
    {
    }
}

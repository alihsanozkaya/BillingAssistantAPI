
using BillingAssistant.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Abstract
{
    public interface IPaymentService
    {
        Task<IDataResult<string>> CreatePaymentIntentAsync(int amount);
    }
}
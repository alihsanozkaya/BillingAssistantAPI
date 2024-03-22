using BillingAssistant.Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Abstract
{
    public interface IOcrService
    {
        public Task<IResult> ReadOCr(IFormFile formFile);
    }
}

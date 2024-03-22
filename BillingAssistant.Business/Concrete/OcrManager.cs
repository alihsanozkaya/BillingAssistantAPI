using BillingAssistant.Business.Abstract;
using BillingAssistant.Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Business.Concrete
{
    public class OcrManager : IOcrService
    {
        public OcrManager() { }

        public Task<IResult> ReadOCr(IFormFile formFile)
        {
            throw new NotImplementedException();
        }
    }
}

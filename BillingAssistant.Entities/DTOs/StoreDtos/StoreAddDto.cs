using BillingAssistant.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.StoreDtos
{
    public class StoreAddDto : IDto
    {
        public string StoreName { get; set; }
    }
}

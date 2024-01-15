using BillingAssistant.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.StoreDtos
{
    public class StoresDto : IDto
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
    }
}

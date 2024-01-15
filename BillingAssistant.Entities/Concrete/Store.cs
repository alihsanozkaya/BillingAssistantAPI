using BillingAssistant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.Concrete
{
    public class Store : AuditableEntity
    {
        public string StoreName { get; set; }
    }
}
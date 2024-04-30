using BillingAssistant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.Concrete
{
    public class Product : AuditableEntity
    {
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public double Price { get; set; }
    }
}
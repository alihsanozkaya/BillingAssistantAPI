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
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
using BillingAssistant.Core.Entities;
using BillingAssistant.Core.Entities.Concrete.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.Concrete
{
    public class Order : AuditableEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string ImageUrl { get; set; }
    }
}
using BillingAssistant.Core.Entities;
using BillingAssistant.Core.Entities.Concrete.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.Concrete;

public class Invoice : AuditableEntity
{
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int StoreId { get; set; }
    public virtual Store Store { get; set; }
    public string ImageUrl { get; set; }
}
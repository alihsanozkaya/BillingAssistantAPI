using BillingAssistant.Core.Entities.Concrete.Auth;
using BillingAssistant.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.InvoiceDtos;

public class InvoicesDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int StoreId { get; set; }
    public virtual Store Store { get; set; }
    public string ImageUrl { get; set; }
}

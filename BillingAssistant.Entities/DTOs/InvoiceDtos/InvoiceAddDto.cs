using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.InvoiceDtos;

public class InvoiceAddDto
{
    public int UserId { get; set; }
    public int StoreId { get; set; } = 1;
    public string ImageUrl { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.InvoiceDtos;

public class InvoiceUpdateDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int StoreId { get; set; }
    public string ImageUrl { get; set; }
}

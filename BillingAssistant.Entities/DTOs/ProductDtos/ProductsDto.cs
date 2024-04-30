using BillingAssistant.Core.Entities.Abstract;
using BillingAssistant.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.ProductDtos
{
    public class ProductsDto : IDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public string Name { get; set; }
        public int Unit { get; set; }
        public double Price { get; set; }
    }
}

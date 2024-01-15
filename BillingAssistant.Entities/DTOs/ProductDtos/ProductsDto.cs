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
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}

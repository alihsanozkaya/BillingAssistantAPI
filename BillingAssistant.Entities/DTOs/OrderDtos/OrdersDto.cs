using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.OrderDtos
{
    public class OrdersDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }

        public string ImageUrl { get; set; }
    }
}
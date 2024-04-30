using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.OrderDtos
{
    public class OrderAddDto
    {
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
    }
}
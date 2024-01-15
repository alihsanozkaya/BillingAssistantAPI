using BillingAssistant.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.CategoryDtos
{
    public class CategoryAddDto : IDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}

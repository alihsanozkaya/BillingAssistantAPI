﻿using BillingAssistant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.Concrete
{
    public class Category : AuditableEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
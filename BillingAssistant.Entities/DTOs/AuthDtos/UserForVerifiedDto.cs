using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.AuthDtos
{
    public class UserForVerifiedDto
    {
        public string Email { get; set; }
        public bool Verified { get; set; }
    }
}

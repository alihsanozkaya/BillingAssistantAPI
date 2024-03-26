using BillingAssistant.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.AuthDtos
{
    public class UserForUpdateProfileDto : IDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ad girilmesi zorunludur")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad girilmesi zorunludur")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "E-mail girilmesi zorunludur")]
        public string Email { get; set; }
    }
}

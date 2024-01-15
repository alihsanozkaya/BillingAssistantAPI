using BillingAssistant.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAssistant.Entities.DTOs.AuthDtos
{
    public class UserForRegisterDto : IDto
    {
        [Required(ErrorMessage = "Ad girilmesi zorunludur")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyad girilmesi zorunludur")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "E-mail girilmesi zorunludur")]
        public string Email { get; set; }
        [MinLength(6)]
        [Required(ErrorMessage = "Şifre girilmesi zorunludur")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Şifreler birbirleriyle eşleşmemektedir")]
        [Required(ErrorMessage = "Tekrardan şifre girilmesi zorunludur")]
        public string ConfirmPassword { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Client.ViewModels.Authentication
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password do not match!")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
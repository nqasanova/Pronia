using System;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Client.ViewModels.Account
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Current Password is required!")]
        public string CurrentPasword { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }

        public UserViewModel(string firstName, string lastName, string email, string currentPasword, string password, string confirmPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CurrentPasword = currentPasword;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public UserViewModel() { }
    }
}
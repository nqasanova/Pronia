using System;
namespace Pronia.Areas.Admin.ViewModels.Contact
{
    public class ListItemViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public ListItemViewModel(string firstName, string lastName, string phone, string email, string message)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            Message = message;
        }
    }
}
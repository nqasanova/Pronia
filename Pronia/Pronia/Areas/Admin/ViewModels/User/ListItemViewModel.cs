using System;
namespace Pronia.Areas.Admin.ViewModels.User
{
    public class ListItemViewModel
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string? Roles { get; set; }

        public ListItemViewModel(Guid id, string? firstName, string? lastName, string? email, DateTime createdAt, DateTime updatedAt, string? roles)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Roles = roles;
        }
    }
}
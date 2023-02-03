using System;
using Pronia.Areas.Admin.ViewModels.Role;

namespace Pronia.Areas.Admin.ViewModels.User
{
    public class UpdateViewModel
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public int? RoleId { get; set; }
        public List<RoleViewModel>? Roles { get; set; }
    }
}
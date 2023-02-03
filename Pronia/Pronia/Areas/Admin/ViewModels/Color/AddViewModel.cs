using System;
namespace Pronia.Areas.Admin.ViewModels.Color
{
    public class AddViewModel
    {
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public AddViewModel(string name, DateTime createdAt, DateTime updatedAt)
        {
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
using System;
namespace Pronia.Areas.Admin.ViewModels.Category
{
    public class AddViewModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public AddViewModel(string name, int? parentId, DateTime createdAt, DateTime updatedAt)
        {
            Name = name;
            ParentId = parentId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
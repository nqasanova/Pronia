using System;
namespace Pronia.Areas.Admin.ViewModels.Feedback
{
    public class AddViewModel
    {
        public int? Id { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string Role { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }
    }
}
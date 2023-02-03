using System;
namespace Pronia.Areas.Admin.ViewModels.Reward
{
	public class AddViewModel
	{
        public int? Id { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
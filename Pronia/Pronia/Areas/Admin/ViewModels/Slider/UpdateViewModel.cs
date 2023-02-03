using System;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Admin.ViewModels.Slider
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string ButtonName { get; set; }
        [Required]
        public string ButtonURL { get; set; }
        [Required]
        public int Order { get; set; }

        public IFormFile Image { get; set; }
        public string ImageURL { get; set; }
    }
}
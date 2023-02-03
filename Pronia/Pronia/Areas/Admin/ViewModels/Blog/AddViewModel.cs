using System;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Admin.ViewModels.Blog
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<int> TagIds { get; set; }

        [Required]
        public List<int> CategoryIds { get; set; }

        public List<TagListItemViewModel>? Tags { get; set; }
        public List<CategoryListItemViewModel>? Categories { get; set; }
    }
}
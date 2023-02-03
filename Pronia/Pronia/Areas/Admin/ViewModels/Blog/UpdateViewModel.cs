using System;
using System.ComponentModel.DataAnnotations;
using static Pronia.Areas.Admin.ViewModels.Blog.AddViewModel;

namespace Pronia.Areas.Admin.ViewModels.Blog
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<int> TagIds { get; set; }

        public List<int> CategoryIds { get; set; }

        public List<TagListItemViewModel>? Tags { get; set; }
        public List<CategoryListItemViewModel>? Categories { get; set; }
    }
}
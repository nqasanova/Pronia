using System;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Admin.ViewModels.Product.Add
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public List<int> TagIds { get; set; }
        [Required]
        public List<int> SizeIds { get; set; }
        [Required]
        public List<int> ColorIds { get; set; }
        [Required]
        public List<int> CategoryIds { get; set; }

        public List<TagListItemViewModel>? Tags { get; set; }
        public List<SizeListItemViewModel>? Sizes { get; set; }
        public List<ColorListItemViewModel>? Colors { get; set; }
        public List<CategoryListItemViewModel>? Categories { get; set; }
    }
}
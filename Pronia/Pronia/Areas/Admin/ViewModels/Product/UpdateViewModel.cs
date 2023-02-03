using System;
using System.ComponentModel.DataAnnotations;
using Pronia.Areas.Admin.ViewModels.Product.Add;

namespace Pronia.Areas.Admin.ViewModels.Product
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        public int? Rate { get; set; }

        public List<int> TagIds { get; set; }
        public List<int> SizeIds { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> CategoryIds { get; set; }

        public List<TagListItemViewModel>? Tags { get; set; }
        public List<SizeListItemViewModel>? Sizes { get; set; }
        public List<ColorListItemViewModel>? Colors { get; set; }
        public List<CategoryListItemViewModel>? Categories { get; set; }
    }
}
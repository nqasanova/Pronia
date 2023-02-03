using System;
using Pronia.Areas.Admin.ViewModels.Product.Add;

namespace Pronia.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<TagViewModel> Tags { get; set; }
        public List<SizeViewModel> Sizes { get; set; }
        public List<ColorViewModel> Colors { get; set; }
        public List<CategoryViewModel> Categories { get; set; }

        public ListItemViewModel(int id, string name, string description, decimal price, DateTime createdAt, List<TagViewModel> tags, List<SizeViewModel> sizes, List<ColorViewModel> colors, List<CategoryViewModel> categories)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            Tags = tags;
            Sizes = sizes;
            Colors = colors;
            Categories = categories;
        }

        public class TagViewModel
        {
            public string Name { get; set; }
            public int Id { get; }

            public TagViewModel(string name)
            {
                Name = name;
            }

            public TagViewModel(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class SizeViewModel
        {
            public string Name { get; set; }

            public SizeViewModel(string name)
            {
                Name = name;
            }
        }

        public class ColorViewModel
        {
            public string Name { get; set; }

            public ColorViewModel(string name)
            {
                Name = name;
            }
        }

        public class CategoryViewModel
        {
            public string Name { get; set; }
            public string Parent { get; set; }

            public CategoryViewModel(string name, string parent)
            {
                Name = name;
                Parent = parent;
            }
        }
    }
}
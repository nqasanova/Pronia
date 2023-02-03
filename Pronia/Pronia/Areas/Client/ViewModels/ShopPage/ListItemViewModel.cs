using System;
using static Pronia.Areas.Admin.ViewModels.Product.ListItemViewModel;

namespace Pronia.Areas.Client.ViewModels.ShopPage
{
	public class ListItemViewModel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public string ImageURL { get; set; }

        public List<TagViewModel> Tags { get; set; }
        public List<SizeViewModeL> Sizes { get; set; }
        public List<ColorViewModeL> Colors { get; set; }
        public List<CategoryViewModeL> Categories { get; set; }

        public ListItemViewModel(int id, string name, string description, decimal price, DateTime createdAt, string imageURL, List<TagViewModel> tags, List<SizeViewModeL> sizes, List<ColorViewModeL> colors, List<CategoryViewModeL> categories)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            ImageURL = imageURL;
            Tags = tags;
            Sizes = sizes;
            Colors = colors;
            Categories = categories;
        }

        public class TagViewModel
        {
            public string Name { get; set; }

            public TagViewModel(string name)
            {
                Name = name;
            }
        }

        public class SizeViewModeL
        {
            public string Name { get; set; }

            public SizeViewModeL(string name)
            {
                Name = name;
            }
        }

        public class ColorViewModeL
        {
            public string Name { get; set; }

            public ColorViewModeL(string name)
            {
                Name = name;
            }
        }

        public class CategoryViewModeL
        {
            public string Name { get; set; }
            public string ParentName { get; set; }

            public CategoryViewModeL(string name, string parentName)
            {
                Name = name;
                ParentName = parentName;
            }
        }
    }
}
using System;
namespace Pronia.Areas.Client.ViewModels.ShopPage
{
    public class ShopIndexViewModel
    {
        public List<TagListItemViewModel> Tags { get; set; }
        public List<SizeListItemViewModel> Sizes { get; set; }
        public List<ColorListItemViewModel> Colors { get; set; }
        public List<CategoryListItemViewModel> Categories { get; set; }

        public ShopIndexViewModel(List<TagListItemViewModel> tags, List<SizeListItemViewModel> sizes, List<ColorListItemViewModel> colors, List<CategoryListItemViewModel> categories)
        {
            Tags = tags;
            Sizes = sizes;
            Colors = colors;
            Categories = categories;
        }
    }

    public class TagListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TagListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class SizeListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SizeListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class ColorListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ColorListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class CategoryListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
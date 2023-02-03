using System;
namespace Pronia.Areas.Client.ViewModels.BlogPage
{
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
}
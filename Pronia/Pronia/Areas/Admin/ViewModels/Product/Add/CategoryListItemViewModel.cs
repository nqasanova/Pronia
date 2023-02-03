using System;
namespace Pronia.Areas.Admin.ViewModels.Product.Add
{
    public class CategoryListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CategoryListItemViewModel(int id, string name)
        {
            Id = Id;
            Name = name;
        }
    }
}
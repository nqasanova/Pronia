using System;
namespace Pronia.Areas.Admin.ViewModels.Product.Add
{
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
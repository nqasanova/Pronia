using System;
namespace Pronia.Areas.Admin.ViewModels.Product.Add
{
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
}
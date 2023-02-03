using System;
namespace Pronia.Areas.Client.ViewModels.Home.Index
{
    public class ProductListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }

        public ProductListItemViewModel(int id, string name, decimal price, string imageURL)
        {
            Id = id;
            Name = name;
            Price = price;
            ImageURL = imageURL;
        }
    }
}
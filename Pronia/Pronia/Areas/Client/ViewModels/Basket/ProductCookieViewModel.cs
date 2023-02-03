using System;
namespace Pronia.Areas.Client.ViewModels.Basket
{
    public class ProductCookieViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageURL { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

        public ProductCookieViewModel(int id, string? title, string? imageURL, int quantity, decimal price, decimal total)
        {
            Id = id;
            Title = title;
            ImageURL = imageURL;
            Quantity = quantity;
            Price = price;
            Total = total;
        }
    }
}
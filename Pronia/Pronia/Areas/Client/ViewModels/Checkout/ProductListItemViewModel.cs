using System;
namespace Pronia.Areas.Client.ViewModels.Checkout
{
    public class ProductListItemViewModel
    {
        public int ProductId { get; set; }
        public List<ListItem>? Products { get; set; }

        public class ListItem
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal Total { get; set; }

            public ListItem(int id, string name, int quantity, decimal price, decimal total)
            {
                Id = id;
                Name = name;
                Quantity = quantity;
                Price = price;
                Total = total;
            }
        }
    }
}
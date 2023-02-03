using System;
namespace Pronia.Areas.Client.ViewModels.Product
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public ListItemViewModel(int id, string name, decimal price, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Price = price;
            CreatedAt = createdAt;
        }
    }
}
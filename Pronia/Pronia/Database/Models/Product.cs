using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Product : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductTag> ProductTags { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
        public List<BasketProduct>? BasketProducts { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
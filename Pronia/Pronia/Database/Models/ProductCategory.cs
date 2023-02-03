using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class ProductCategory : BaseEntity<int>, IAuditable
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
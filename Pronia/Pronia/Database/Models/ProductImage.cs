using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class ProductImage : BaseEntity<int>, IAuditable
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
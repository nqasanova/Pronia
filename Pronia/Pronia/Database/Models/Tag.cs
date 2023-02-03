using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Tag : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<ProductTag> ProductTags { get; set; }
    }
}
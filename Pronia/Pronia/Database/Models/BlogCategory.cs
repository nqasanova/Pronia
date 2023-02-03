using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class BlogCategory : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }

        public int? ParentId { get; set; }
        public BlogCategory? Parent { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<BlogCategory> Categories { get; set; }
        public List<BlogandBlogCategory> BlogCategories { get; set; }
    }
}
using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class BlogTag : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<BlogandBlogTag> Tags { get; set; }
    }
}
using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Blog : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<BlogFile> BlogFiles { get; set; }
        public List<BlogandBlogTag> BlogandBlogTags { get; set; }
        public List<BlogandBlogCategory> BlogandBlogCategories { get; set; }
    }
}
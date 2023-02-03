using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class BlogandBlogCategory : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory Category { get; set; }
    }
}
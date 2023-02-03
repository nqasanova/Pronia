using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class BlogandBlogTag : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

        public int BlogTagId { get; set; }
        public BlogTag Tag { get; set; }
    }
}
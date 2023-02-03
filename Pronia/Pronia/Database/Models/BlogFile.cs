using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class BlogFile : BaseEntity<int>, IAuditable
    {
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }

        public string? FileName { get; set; }
        public string? FileNameInFileSystem { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
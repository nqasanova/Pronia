using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Feedback : BaseEntity<int>, IAuditable
    {
        public string FullName { get; set; }
        public string Content { get; set; }
        public string Role { get; set; }

        public string ProfilePhoteImageName { get; set; }
        public string ProfilePhoteInFileSystem { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
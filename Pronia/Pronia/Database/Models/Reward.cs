using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Reward : BaseEntity<int>, IAuditable
    {
        public string ImageName { get; set; }
        public string ImageNameInFileSystem { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
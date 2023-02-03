using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class About : BaseEntity<int>, IAuditable
    {
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
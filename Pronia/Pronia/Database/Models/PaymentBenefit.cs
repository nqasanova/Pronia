using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class PaymentBenefit : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string ImageName { get; set; }
        public string ImageNameInFileSystem { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
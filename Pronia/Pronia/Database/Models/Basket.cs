using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Basket : BaseEntity<int>, IAuditable
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<BasketProduct>? BasketProducts { get; set; }
    }
}
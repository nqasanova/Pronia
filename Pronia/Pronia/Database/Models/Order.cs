﻿using System;
using Pronia.Database.Models.Common;
using Pronia.Database.Models.Enums;

namespace Pronia.Database.Models
{
    public class Order : BaseEntity<string>, IAuditable
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
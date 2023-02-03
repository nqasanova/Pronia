using System;
using Pronia.Database.Models.Enums;

namespace Pronia.Areas.Admin.ViewModels.Order
{
    public class ListItemViewModel
    {
        public string Id { get; set; }

        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ListItemViewModel(string id, decimal total, OrderStatus status, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Total = total;
            Status = status;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
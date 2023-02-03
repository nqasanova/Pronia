using System;
using Pronia.Database.Models.Enums;

namespace Pronia.Areas.Client.ViewModels.Account
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public decimal Total { get; set; }

        public int OrderCount { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime Date { get; set; }

        public OrderViewModel(string id, decimal total, int orderCount, OrderStatus status, DateTime date)
        {
            Id = id;
            Total = total;
            OrderCount = orderCount;
            Status = status;
            Date = date;
        }
    }
}
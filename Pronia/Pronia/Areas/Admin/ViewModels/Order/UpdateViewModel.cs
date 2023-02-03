using System;
using Pronia.Database.Models.Enums;

namespace Pronia.Areas.Admin.ViewModels.Order
{
    public class UpdateViewModel
    {
        public string Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
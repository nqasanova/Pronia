using System;
namespace Pronia.Contracts.Order
{
    public enum OrderStatus
    {
        Created = 0,
        Accepted = 1,
        Rejected = 2,
        Sent = 4,
        Completed = 8
    }

    public static class StatusCode
    {
        public static string GetStatusCode(this OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Created:
                    return "Created";
                case OrderStatus.Accepted:
                    return "Confirmed";
                case OrderStatus.Rejected:
                    return "Rejected";
                case OrderStatus.Sent:
                    return "Sended";
                case OrderStatus.Completed:
                    return "Completed";
                default:
                    throw new Exception("Entered status code could not be found!");
            }
        }
    }
}
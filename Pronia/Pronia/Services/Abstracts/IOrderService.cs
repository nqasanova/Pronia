using System;
namespace Pronia.Services.Abstracts
{
    public interface IOrderService
    {
        Task<string> GenerateUniqueTrackingCodeAsync();
    }
}
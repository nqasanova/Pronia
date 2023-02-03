using System;
using Pronia.Areas.Client.ViewModels.Basket;
using Pronia.Database.Models;

namespace Pronia.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<ProductCookieViewModel>> AddBasketProductAsync(Product product);
    }
}
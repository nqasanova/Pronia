using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.Basket;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "Cart")]
    public class Cart : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public Cart(DataContext dataContext, IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<ProductCookieViewModel> viewModel = null)
        {
            if (_userService.IsAuthenticated)
            {
                var model = await _dataContext.BasketProducts
                    .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
                    .Select(bp =>
                        new ProductCookieViewModel(
                            bp.ProductId,
                            bp.Product!.Name,
                            bp.Product.ProductImages.Take(1).FirstOrDefault()! != null
                            ? _fileService.GetFileUrl(bp.Product.ProductImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Product)
                            : String.Empty,
                            bp.Quantity,
                            bp.Product.Price,
                            bp.Product.Price * bp.Quantity))
                    .ToListAsync();

                return View(model);
            }

            var productsCookieValue = HttpContext.Request.Cookies["products"];
            var productsCookieViewModel = new List<ProductCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productsCookieValue);
            }

            if (viewModel != null)
            {
                return View(viewModel);
            }

            return View(productsCookieViewModel);
        }
    }
}
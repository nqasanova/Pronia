﻿using System;
using Microsoft.AspNetCore.Mvc;
using Pronia.Areas.Client.ViewModels.Basket;
using Pronia.Database;
using Pronia.Services.Abstracts;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Pronia.Contracts.File;

namespace Pronia.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopCart")]
    public class ShopCart : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public ShopCart(DataContext dataContext, IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<ProductCookieViewModel>? viewModels = null)
        {
            // Case 1 : Qeydiyyat kecilib, o zaman bazadan gotur
            if (_userService.IsAuthenticated)
            {
                var model = await _dataContext.BasketProducts
                    .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
                    .Select(bp =>
                    new ProductCookieViewModel(
                            bp.ProductId,
                            bp.Product!.Name,
                            bp.Product.ProductImages.Take(1).FirstOrDefault() != null
                            ? _fileService.GetFileUrl(bp.Product.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                            : String.Empty,
                            bp.Quantity,
                            bp.Product.Price,
                            bp.Product.Price * bp.Quantity))
                    .ToListAsync();

                return View(model);
            }

            //Case 2: Argument olaraq actiondan gonderilib
            if (viewModels is not null)
            {
                return View(viewModels);
            }

            //Case 3: Argument gonderilmeyib bu zaman cookiden oxu
            var productsCookieValue = HttpContext.Request.Cookies["products"];
            var productsCookieViewModel = new List<ProductCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productsCookieValue);
            }

            return View(productsCookieViewModel);
        }
    }
}
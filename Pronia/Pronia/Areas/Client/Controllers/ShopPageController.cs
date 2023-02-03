using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewComponents;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Abstracts;
using Pronia.Areas.Client.ViewModels.ShopPage;
using static Pronia.Areas.Client.ViewModels.ShopPage.ListItemViewModel;

namespace Pronia.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shoppage")]
    public class ShopPageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public ShopPageController(DataContext dataContext, IBasketService basketService, IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
            _fileService = fileService;
        }

        [HttpGet("index", Name = "client-shoppage-index")]
        public async Task<IActionResult> Index(string searchBy, string search, [FromQuery] int? categoryId, [FromQuery] int? colorId, [FromQuery] int? tagId)
        {
            var productsQuery = _dataContext.Products.AsQueryable();

            if (searchBy == "Name")
            {
                productsQuery = productsQuery.Where(p => p.Name.StartsWith(search) || Convert.ToString(p.Price).StartsWith(search) || search == null);
            }

            else if (categoryId is not null || colorId is not null || tagId is not null)
            {
                productsQuery = productsQuery
                    .Include(p => p.ProductTags)
                    .Include(p => p.ProductColors)
                    .Include(p => p.ProductCategories)
                    .Where(p => tagId == null || p.ProductTags!.Any(pt => pt.TagId == tagId))
                    .Where(p => colorId == null || p.ProductColors!.Any(pc => pc.ColorId == colorId))
                    .Where(p => categoryId == null || p.ProductCategories!.Any(pc => pc.CategoryId == categoryId));
            }

            else
            {
                productsQuery = productsQuery.OrderBy(p => p.Price);
            }

            var newProduct = await productsQuery.Select(p => new ListItemViewModel(p.Id, p.Name, p.Content, p.Price, p.CreatedAt,
                               p.ProductImages.Take(1).FirstOrDefault() != null
                               ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                               : String.Empty,
                               p.ProductTags.Select(p => p.Tag).Select(p => new TagViewModel(p.Name)).ToList(),
                               p.ProductSizes.Select(p => p.Size).Select(p => new SizeViewModeL(p.Name)).ToList(),
                               p.ProductColors.Select(p => p.Color).Select(p => new ColorViewModeL(p.Name)).ToList(),
                               p.ProductCategories.Select(p => p.Category).Select(p => new CategoryViewModeL(p.Name, p.Parent.Name)).ToList())).ToListAsync();

            return View(newProduct);

        }
    }
}
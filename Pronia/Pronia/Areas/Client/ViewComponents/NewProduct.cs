using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.Basket;
using Pronia.Areas.Client.ViewModels.Home.Index;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "NewProduct")]
    public class NewProduct : ViewComponent
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public NewProduct(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new IndexViewModel
            {
                Products = await _dbContext.Products.OrderByDescending(p => p.CreatedAt).Take(4)
                 .Select(b => new ProductListItemViewModel(
                     b.Id,
                     b.Name,
                     b.Price,
                     b.ProductImages!.Take(1)!.FirstOrDefault()! != null
                         ? _fileService.GetFileUrl(b.ProductImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Product)
                         : string.Empty))
                 .ToListAsync()
            };

            return View(model);
        }
    }
}
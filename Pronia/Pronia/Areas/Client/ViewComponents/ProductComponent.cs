using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.Home.Index;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ProductComponent")]
    public class ProductComponent : ViewComponent
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public ProductComponent(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string querry)
        {
            var model = new IndexViewModel();

            if (querry == "BestSeller")
            {
                var bestSellerIds = await _dbContext.OrderProducts
                    .GroupBy(op => op.ProductId)
                    .OrderByDescending(p => p.Count())
                    .Take(6)
                    .Select(x => x.Key)
                    .ToListAsync();

                model.Products = await _dbContext.Products.OrderByDescending(p => p.Id).Where(p => bestSellerIds.Contains(p.Id))
                    .Select(p => new ProductListItemViewModel(p.Id, p.Name, p.Price,
                        p.ProductImages.Take(1).FirstOrDefault()! != null
                        ? _fileService.GetFileUrl(p.ProductImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Product) : String.Empty)).
                        ToListAsync();

                return View(model);
            }

            model.Products = await _dbContext.Products
             .Select(b => new ProductListItemViewModel(
                 b.Id,
                 b.Name,
                 b.Price,
                 b.ProductImages!.Take(1)!.FirstOrDefault()! != null
                     ? _fileService.GetFileUrl(b.ProductImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Product)
                     : string.Empty))
             .ToListAsync();

            return View(model);
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.ProductDetails;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Abstracts;
using Pronia.Areas.Client.ViewModels.Product;
using System.Linq;
using Pronia.Areas.Client.ViewModels.Home.Index;

namespace Pronia.Areas.Client.Controllers
{
    [Area("client")]
    [Route("productdetails")]
    public class ProductDetailsController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public ProductDetailsController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("index/{id}", Name = "client-productdetails-index")]
        public async Task<IActionResult> Index(int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.ProductTags)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null) return NotFound();

            var catProducts = await _dbContext
                .ProductCategories.GroupBy(pc => pc.CategoryId).Select(pc => pc.Key).ToListAsync();


            var model = new ProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Content,
                Price = product.Price,
                PaymentBenefits = await _dbContext.PaymentBenefits.Select(p => new PaymentBListItemViewModel(
                  p.Id,
                  p.Title,
                  p.Content,
                  _fileService.GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.PaymentBenefit))).ToListAsync(),

                Tags = _dbContext.ProductTags.Include(pt => pt.Tag).Where(pt => pt.ProductId == product.Id)
                      .Select(ps => new ProductDetailsViewModel.TagViewModel(ps.Tag.Id, ps.Tag.Name)).ToList(),

                Colors = _dbContext.ProductColors.Include(pc => pc.Color).Where(pc => pc.ProductId == product.Id)
                          .Select(pc => new ProductDetailsViewModel.ColorViewModel(pc.Color.Id, pc.Color.Name)).ToList(),

                Sizes = _dbContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                       .Select(ps => new ProductDetailsViewModel.SizeViewModel(ps.Size.Id, ps.Size.Name)).ToList(),

                Images = _dbContext.ProductImages.Where(p => p.ProductId == product.Id)
                .Select(p => new ProductDetailsViewModel.ImageViewModel
                (_fileService.GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.Product))).ToList(),

                Catagories = _dbContext.ProductCategories.Include(ps => ps.Category).Where(ps => ps.ProductId == product.Id)
                         .Select(ps => new ProductDetailsViewModel.CatagoryViewModel(ps.Category.Id, ps.Category.Name)).ToList(),

               // Products = await _dbContext.ProductCategories.Include(p => p.Product).Where(pc => pc.ProductId != product.Id)
               // .Select(pc => new ListItemViewModel(pc.ProductId, pc.Product.Name, pc.Product.Price, pc.Product.CreatedAt,
               // pc.Product.ProductImages.Take(1).FirstOrDefault() != null
               // ? _fileService.GetFileUrl(pc.Product.ProductImages.Take(1).FirstOrDefault().ImageNameInFileSystem, UploadDirectory.Product)
               // : String.Empty
               //)).ToListAsync(),
            };

            return View(model);
        }
    }
}
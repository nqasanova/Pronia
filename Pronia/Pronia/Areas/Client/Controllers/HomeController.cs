using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.About;
using Pronia.Areas.Client.ViewModels.Basket;
using Pronia.Areas.Client.ViewModels.Home.Index;
using Pronia.Areas.Client.ViewModels.Home.Modal;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Abstracts;
using PaymentBListItemViewModel = Pronia.Areas.Client.ViewModels.Home.Index.PaymentBListItemViewModel;
using RewardListItemViewModel = Pronia.Areas.Client.ViewModels.Home.Index.RewardListItemViewModel;

namespace Pronia.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public HomeController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync()
        {
            var model = new IndexViewModel
            {
                Sliders = await _dbContext.Sliders.OrderBy(s => s.Order).Select(b => new SliderListItemViewModel(
                    b.Title,
                    b.Content,
                    b.ButtonName,
                    b.ButtonURL,
                    b.ImageName,
                    _fileService.GetFileUrl(b.ImageNameInFileSystem, UploadDirectory.Slider),
                    b.Order))
                .ToListAsync(),

                PaymentBenefits = await _dbContext.PaymentBenefits.Select(p => new PaymentBListItemViewModel(
                    p.Id,
                    p.Title,
                    p.Content,
                    _fileService.GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.PaymentBenefit)))
                .ToListAsync(),

                Feedbacks = await _dbContext.Feedbacks.Select(f => new FeedbackListItemViewModel(
                    f.Id,
                    f.FullName,
                    f.Content,
                    f.Role,
                   _fileService.GetFileUrl(f.ProfilePhoteInFileSystem, UploadDirectory.Feedback)))
                .ToListAsync(),

                Rewards = await _dbContext.Rewards.Select(r => new RewardListItemViewModel(
                    r.Id,
                    _fileService.GetFileUrl(r.ImageNameInFileSystem, UploadDirectory.Reward)
                    ))
                .ToListAsync(),

                Blogs = await _dbContext.Blogs.Include(b => b.BlogFiles).Select(b => new BlogListItemViewModel(
                b.Id,
                b.Name,
                b.Description,

                b.BlogFiles!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.BlogFiles.Take(1).FirstOrDefault()!.FileNameInFileSystem, UploadDirectory.Blog)
                : String.Empty,
                b.BlogFiles.FirstOrDefault().IsImage,
                b.BlogFiles.FirstOrDefault().IsVideo,
                b.CreatedAt))
                .ToListAsync(),
            };
            return View(model);
        }

        [HttpGet("modal/{id}", Name = "product-modal")]
        public async Task<ActionResult> ModalAsync(int id)
        {
            var product = await _dbContext.Products.Include(p => p.ProductImages)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null) return NotFound();

            var model = new ModalViewModel(product.Id, product.Name, product.Content, product.Price,
                product.ProductImages!.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(product.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                : String.Empty,
                _dbContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                .Select(ps => new ModalViewModel.SizeViewModel(ps.Size.Id, ps.Size.Name)).ToList(),
                _dbContext.ProductColors.Include(pc => pc.Color).Where(pc => pc.ProductId == product.Id)
                .Select(pc => new ModalViewModel.ColorViewModel(pc.Color.Id, pc.Color.Name)).ToList()
                );

            return PartialView("~/Areas/Client/Views/Shared/Partials/_ModelPartial.cshtml", model);
        }

        [HttpGet("indexsearch", Name = "client-homesearch-index")]
        public async Task<IActionResult> Search(string searchBy, string search)
        {

            return RedirectToAction("Index", "ShopPage", new { searchBy = searchBy, search = search });

        }
    }
}
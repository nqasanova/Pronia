using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.About;
using Pronia.Areas.Client.ViewModels.About;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Abstracts;
using ListItemViewModel = Pronia.Areas.Client.ViewModels.About.ListItemViewModel;

namespace Pronia.Areas.Client.Controllers
{
    [Area("client")]
    [Route("about")]
    public class AboutController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public AboutController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("page", Name = "client-about")]
        public async Task<IActionResult> About()
        {
            var model = new AboutViewModel
            {
                Abouts = await _dbContext.Abouts.Select(b => new ListItemViewModel(b.Content))
                .ToListAsync(),

                Rewards = await _dbContext.Rewards.Select(r => new RewardListItemViewModel(r.Id, _fileService.GetFileUrl(r.ImageNameInFileSystem, UploadDirectory.Reward)))
                .ToListAsync(),

                PaymentBenefits = await _dbContext.PaymentBenefits.Select(p => new PaymentBListItemViewModel(p.Id, p.Title, p.Content, _fileService.GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.PaymentBenefit)))
                .ToListAsync(),
            };

            return View(model);
        }
    }
}
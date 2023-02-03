using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Abstracts;
using Pronia.Areas.Admin.ViewModels.Reward;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/reward")]
    [Authorize(Roles = "admin")]
    public class RewardController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public RewardController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-reward-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Rewards
                .Select(u => new ListItemViewModel(u.Id, _fileService.GetFileUrl(u.ImageNameInFileSystem, UploadDirectory.Reward), u.CreatedAt, u.UpdatedAt))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-reward-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-reward-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Reward);

            await AddReward(model.Image!.FileName, imageNameInSystem);

            return RedirectToRoute("admin-reward-list");

            async Task AddReward(string imageName, string imageNameInSystem)
            {
                var reward = new Reward
                {
                    ImageName = imageName,
                    ImageNameInFileSystem = imageNameInSystem,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                await _dataContext.Rewards.AddAsync(reward);
                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-reward-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var reward = await _dataContext.Rewards.FirstOrDefaultAsync(r => r.Id == id);
            if (reward is null) return NotFound();

            var model = new AddViewModel
            {
                Id = reward.Id,
                ImageURL = _fileService.GetFileUrl(reward.ImageNameInFileSystem, UploadDirectory.Reward)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-reward-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            var reward = await _dataContext.Rewards.FirstOrDefaultAsync(r => r.Id == model.Id);
            if (reward is null) return NotFound();

            if (!ModelState.IsValid) return View(model);

            if (model.Image != null)
            {
                await _fileService.DeleteAsync(reward.ImageNameInFileSystem, UploadDirectory.Reward);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Reward);
                await UpdateRewardAsync(model.Image.FileName, imageFileNameInSystem);
            }

            else
            {
                await UpdateRewardAsync(reward.ImageName, reward.ImageNameInFileSystem);
            }

            return RedirectToRoute("admin-reward-list");


            async Task UpdateRewardAsync(string imageName, string imageNameInFileSystem)
            {

                reward.ImageName = imageName;
                reward.ImageNameInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion
    }
}
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Abstracts;
using Pronia.Areas.Admin.ViewModels.Feedback;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/feedback")]
    [Authorize(Roles = "admin")]
    public class FeedBackController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public FeedBackController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-feedback-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Feedbacks
                .Select(u => new ListItemViewModel(u.Id, u.FullName, u.Content, u.Role, _fileService.GetFileUrl(u.ProfilePhoteInFileSystem, UploadDirectory.Feedback), u.CreatedAt, u.UpdatedAt))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-feedback-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-feedback-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Feedback);

            await AddFeedback(model.Image!.FileName, imageNameInSystem);

            return RedirectToRoute("admin-feedback-list");

            async Task AddFeedback(string imageName, string imageNameInSystem)
            {
                var feedback = new Feedback
                {
                    FullName = model.FullName,
                    Role = model.Role,
                    Content = model.Content,
                    ProfilePhoteImageName = imageName,
                    ProfilePhoteInFileSystem = imageNameInSystem,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                await _dataContext.Feedbacks.AddAsync(feedback);
                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-feedback-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var feedback = await _dataContext.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
            if (feedback is null)
            {
                return NotFound();
            }

            var model = new AddViewModel
            {
                Id = feedback.Id,
                FullName = feedback.FullName,
                Content = feedback.Content,
                Role = feedback.Role,
                ImageURL = _fileService.GetFileUrl(feedback.ProfilePhoteInFileSystem, UploadDirectory.Feedback)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-feedback-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            var feedback = await _dataContext.Feedbacks.FirstOrDefaultAsync(f => f.Id == model.Id);

            if (feedback is null) return NotFound();

            if (!ModelState.IsValid) return View(model);

            if (model.Image != null)
            {
                await _fileService.DeleteAsync(feedback.ProfilePhoteInFileSystem, UploadDirectory.Feedback);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Feedback);
                await UpdateFeedBackAsync(model.Image.FileName, imageFileNameInSystem);
            }

            else
            {
                await UpdateFeedBackAsync(feedback.ProfilePhoteImageName, feedback.ProfilePhoteInFileSystem);
            }

            return RedirectToRoute("admin-feedback-list");

            async Task UpdateFeedBackAsync(string imageName, string imageNameInFileSystem)
            {
                feedback.FullName = model.FullName;
                feedback.Content = model.Content;
                feedback.Role = model.Role;
                feedback.ProfilePhoteImageName = imageName;
                feedback.ProfilePhoteInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-feedback-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var feedBack = await _dataContext.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
            if (feedBack is null) return NotFound();

            await _fileService.DeleteAsync(feedBack.ProfilePhoteInFileSystem, UploadDirectory.Feedback);

            _dataContext.Feedbacks.Remove(feedBack);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-feedback-list");
        }

        #endregion
    }
}
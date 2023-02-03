using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.Slider;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    [Authorize(Roles = "admin")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public SliderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sliders.Select(s => new ListItemViewModel
            {
                Id = s.Id,
                Title = s.Title,
                Content = s.Content,
                ImageURL = _fileService.GetFileUrl(s.ImageNameInFileSystem, UploadDirectory.Slider)
            }).ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add-slider", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("add-slider", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var BgImageNameInFileSytstem = await _fileService.UploadAsync(model.Image, UploadDirectory.Slider);

            AddSlider(model.Image.FileName, BgImageNameInFileSytstem);

            return RedirectToRoute("admin-slider-list");

            void AddSlider(string ImageName, string ImageNameInFileSystem)
            {
                var slider = new Slider
                {
                    Title = model.Title,
                    Content = model.Content,
                    ImageName = ImageName,
                    ImageNameInFileSystem = ImageNameInFileSystem,
                    ButtonName = model.ButtonName,
                    ButtonURL = model.ButtonURL,
                    Order = model.Order,
                };

                _dataContext.Add(slider);

                _dataContext.SaveChanges();
            }
        }

        #endregion

        #region Update

        [HttpGet("slider-update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null) return NotFound();
        
            var model = new UpdateViewModel
            {
                Id = slider.Id,
                Title = slider.Title,
                Content = slider.Content,
                ImageURL = _fileService.GetFileUrl(slider.ImageNameInFileSystem, UploadDirectory.Slider),
                ButtonName = slider.ButtonName,
                ButtonURL = slider.ButtonURL,
                Order = slider.Order
            };

            return View(model);
        }

        [HttpPost("slider-update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(s => s.Id == model.Id);

            if (!ModelState.IsValid) GetView(model);

            if (slider is null) return NotFound();

            if (model.Image is not null)
            {
                await _fileService.DeleteAsync(slider.ImageNameInFileSystem, UploadDirectory.Slider);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Slider);
                await UpdateSliderAsync(slider.ImageName, imageFileNameInSystem);
            }

            else
            {
                UpdateSlider();
            }

            IActionResult GetView(UpdateViewModel model)
            {
                model.ImageURL = _fileService.GetFileUrl(slider.ImageNameInFileSystem, UploadDirectory.Slider);
                return View(model);
            }

            void UpdateSlider()
            {
                slider.Title = model.Title;
                slider.Content = model.Content;
                slider.ButtonName = model.ButtonName;
                slider.ButtonURL = model.ButtonURL;
                slider.Order = model.Order;

                _dataContext.SaveChanges();

            };

            async Task UpdateSliderAsync(string BgImageName, string BgImageNameInFileSystem)
            {
                slider.Title = model.Title;
                slider.Content = model.Content;
                slider.ImageName = BgImageName;
                slider.ImageNameInFileSystem = BgImageNameInFileSystem;
                slider.ButtonName = model.ButtonName;
                slider.ButtonURL = model.ButtonURL;
                slider.Order = model.Order;

                await _dataContext.SaveChangesAsync();
            }

            return RedirectToRoute("admin-slider-list");
        }

        #endregion

        #region Delete

        [HttpPost("slider-delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);
            if (slider is null) return NotFound();

            await _fileService.DeleteAsync(slider.ImageNameInFileSystem, UploadDirectory.Slider);

            _dataContext.Sliders.Remove(slider);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }

        #endregion
    }
}
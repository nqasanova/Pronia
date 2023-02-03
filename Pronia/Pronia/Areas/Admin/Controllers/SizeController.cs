using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.Size;
using Pronia.Database;
using Pronia.Database.Models;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/size")]
    [Authorize(Roles = "admin")]
    public class SizeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public SizeController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List

        [HttpGet("list", Name = "admin-size-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sizes.Select(s => new ListItemViewModel(s.Id, s.Name)).ToListAsync();
            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-size-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-size-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var size = new Size
            {
                Name = model.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await _dataContext.Sizes.AddAsync(size);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-size-list");
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(s => s.Id == id);

            if (size is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = size.Id,
                Name = size.Name,
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(s => s.Id == model.Id);
            if (size is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            size.Name = model.Name;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-size-list");
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-size-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(n => n.Id == id);

            if (size is null)
            {
                return NotFound();
            }

            _dataContext.Sizes.Remove(size);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-size-list");
        }

        #endregion
    }
}
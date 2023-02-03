using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.Category;
using Pronia.Database;
using Pronia.Database.Models;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/category")]
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List

        [HttpGet("list", Name = "admin-category-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Categories.Select(c => new ListItemViewModel(c.Id, c.Name)).ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-category-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-category-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = new Category
            {
                ParentId = model.ParentId,
                Name = model.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await _dataContext.Categories.AddAsync(category);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-category-list");
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-category-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = id,
                Name = category.Name,
                ParentId = category.ParentId
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-category-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (category is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!_dataContext.Categories.Any(n => n.Id == model.Id))
            {
                return View(model);
            }

            category.Name = model.Name;
            category.ParentId = model.ParentId;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-category-list");
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-category-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (category is null) return NotFound();

            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-category-list");
        }

        #endregion
    }
}
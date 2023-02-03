using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.Tag;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Areas.Admin.Controllers;
using static Pronia.Areas.Admin.ViewModels.Product.ListItemViewModel;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/Tag")]
    [Authorize(Roles = "admin")]
    public class TagController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public TagController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List

        [HttpGet("list", Name = "admin-tag-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Tags.Select(s => new TagViewModel(s.Id, s.Name)).ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-tag-add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-tag-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tag = new Tag
            {
                Name = model.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await _dataContext.Tags.AddAsync(tag);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-tag-list");
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-tag-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (tag is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-tag-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(t => t.Id == model.Id);
            if (tag is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!_dataContext.Tags.Any(t => t.Id == model.Id))
            {
                return View(model);
            }

            tag.Name = model.Name;
            tag.UpdatedAt = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-tag-list");
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-tag-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (tag is null)
            {
                return NotFound();
            }

            _dataContext.Tags.Remove(tag);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-tag-list");
        }

        #endregion
    }
}
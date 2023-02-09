using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.About;
using Pronia.Database;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/about")]
    [Authorize(Roles = "admin")]
    public class AboutController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public AboutController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List

        [HttpGet("list", Name = "admin-about-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Abouts
                .Select(c => new ListItemViewModel(c.Id, c.Content))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-about-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var about = await _dataContext.Abouts.FirstOrDefaultAsync(a => a.Id == id);

            if (about is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = id,
                Content = about.Content,
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-about-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var color = await _dataContext.Abouts.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (color is null) return NotFound();

            if (!ModelState.IsValid) return View(model);

            if (!_dataContext.Abouts.Any(a => a.Id == model.Id)) return View(model);

            color.Content = model.Content;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-about-list");
        }

        #endregion
    }
}
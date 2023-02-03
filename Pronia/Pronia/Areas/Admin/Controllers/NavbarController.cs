using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Areas.Admin.ViewModels.Navbar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DemoApplication.Controllers.Admin
{
    [Area("admin")]
    [Route("admin/navbar")]
    [Authorize(Roles = "admin")]

    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List

        [HttpGet("list", Name = "admin-navbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Navbars.Select(n => new ListItemViewModel(n.Id, n.Name, n.URL, n.Order, n.IsHeader, n.IsFooter)).ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add-navbar", Name = "admin-add-navbar")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("add-navbar", Name = "admin-add-navbar")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var navbar = new Navbar
            {
                Name = model.Name,
                URL = model.URL,
                Order = model.Order,
                IsHeader = model.IsHeader,
                IsFooter = model.IsFooter,
            };

            await _dataContext.Navbars.AddAsync(navbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }

        #endregion

        #region Update

        [HttpGet("update-navbar/{id}", Name = "admin-update-navbar")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (navbar is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = navbar.Id,
                Name = navbar.Name,
                URL = navbar.URL,
                Order = navbar.Order,
                IsFooter = navbar.IsFooter,
                IsHeader = navbar.IsHeader
            };

            return View(model);
        }

        [HttpPost("update-navbar/{id}", Name = "admin-update-navbar")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, UpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (navbar is null) return NotFound();

            await UpdateNavbar();

            return RedirectToRoute("admin-navbar-list");

            async Task UpdateNavbar()
            {
                navbar.Name = model.Name;
                navbar.URL = model.URL;
                navbar.Order = model.Order;
                navbar.IsHeader = model.IsHeader;
                navbar.IsFooter = model.IsFooter;

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete-navbar/{id}", Name = "admin-delete-navbar")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (navbar is null) return NotFound();

            _dataContext.Navbars.Remove(navbar);


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");

        }

        #endregion
    }
}
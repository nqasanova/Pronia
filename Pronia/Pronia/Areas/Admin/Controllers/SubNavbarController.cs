using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Areas.Admin.ViewModels.SubNavbar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Pronia.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;

namespace Pronia.Controllers.Admin
{
    [Area("admin")]
    [Route("admin/subnavbar")]
    [Authorize(Roles = "admin")]
    public class SubNavbarController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IActionDescriptorCollectionProvider _provider;
        public SubNavbarController(DataContext dataContext, IActionDescriptorCollectionProvider provider)
        {
            _dataContext = dataContext;
            _provider = provider;
        }

        #region List

        [HttpGet("list", Name = "admin-subnavbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.SubNavbars.Select(sn => new ListItemViewModel(sn.Id, sn.Name, sn.URL, sn.Order, $"{sn.Navbar.Name}"))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add-subnavbar", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Navbars = await _dataContext.Navbars.Select(n => new AddViewModel.NavbarViewModel(n.Id, n.Name)).ToListAsync(),

                URLs = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                .Select(u => new AddViewModel.URLViewModel(u!.AttributeRouteInfo.Template)).ToList()
            };

            return View(model);
        }

        [HttpPost("add-subnavbar", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Navbars = await _dataContext.Navbars
                   .Select(a => new AddViewModel.NavbarViewModel(a.Id, a.Name))
                   .ToListAsync();

                model.URLs = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                .Select(u => new AddViewModel.URLViewModel(u!.AttributeRouteInfo.Template)).ToList();

                return View(model);
            }

            var subNavbar = new SubNavbar()
            {
                Name = model.Name,
                URL = model.URL,
                Order = model.Order,
                NavbarId = model.NavbarId,
            };

            await _dataContext.SubNavbars.AddAsync(subNavbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }

        #endregion

        #region Update

        [HttpGet("update-subnavbar/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var subnavbar = await _dataContext.SubNavbars.FirstOrDefaultAsync(b => b.Id == id);
            if (subnavbar is null) return NotFound();

            var model = new UpdateViewModel
            {
                Name = subnavbar.Name,
                URL = subnavbar.URL,
                Order = subnavbar.Order,
                Navbars = await _dataContext.Navbars
                    .Select(n => new UpdateViewModel.NavbarViewModel(n.Id, n.Name))
                    .ToListAsync(),
                NavbarId = subnavbar.NavbarId,
                URLs = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                .Select(u => new UpdateViewModel.URLViewModel(u!.AttributeRouteInfo.Template)).ToList()
            };

            return View(model);
        }

        [HttpPost("update-subnavbar/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync(int id, UpdateViewModel model)
        {
            var subnavbar = await _dataContext.SubNavbars.FirstOrDefaultAsync(b => b.Id == id);
            if (subnavbar is null) return NotFound();

            if (!ModelState.IsValid)
            {
                model.Navbars = await _dataContext.Navbars.Where(n => n.Id == subnavbar.NavbarId)
                    .Select(n => new UpdateViewModel.NavbarViewModel(n.Id, n.Name)).ToListAsync();
                model.URLs = _provider.ActionDescriptors.Items.Where(u => u.RouteValues["Area"] != "admin")
                .Select(u => new UpdateViewModel.URLViewModel(u!.AttributeRouteInfo.Template)).ToList();

                return View(model);
            }

            subnavbar.Name = model.Name;
            subnavbar.URL = model.URL;
            subnavbar.Order = model.Order;
            subnavbar.NavbarId = model.NavbarId;

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-subnavbar-list");
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-subnavbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var subNavbar = await _dataContext.SubNavbars.FirstOrDefaultAsync(sn => sn.Id == id);
            if (subNavbar is null) return NotFound();

            _dataContext.SubNavbars.Remove(subNavbar);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }

        #endregion
    }
}
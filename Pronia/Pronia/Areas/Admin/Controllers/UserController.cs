using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.Role;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Areas.Admin.ViewModels.User;
using BC = BCrypt.Net.BCrypt;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/User")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List

        [HttpGet("list", Name = "admin-user-list")]
        public async Task<IActionResult> ListAsync(int page = 1)
        {
            var model = await _dataContext.Users.Skip((page - 1) * 5).Take(5)
                .OrderByDescending(a => a.CreatedAt)
                .Select(u => new ListItemViewModel(u.Id, u.Email, u.FirstName, u.LastName, u.CreatedAt, u.UpdatedAt, u.Role != null ? u.Role.Name : null))
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_dataContext.Users.Count() / 5);

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-user-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Roles = await _dataContext.Roles.Select(r => new RoleViewModel(r.Id, r.Name)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-user-add")]
        public async Task<IActionResult> AddAsync(AddViewModel? model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await CreateUser();

            await _dataContext.SaveChangesAsync();

            async Task<Basket> CreateBasketAsync()
            {
                var basket = new Basket
                {
                    User = user,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                await _dataContext.Baskets.AddAsync(basket);

                return basket;
            }

            async Task<User> CreateUser()
            {
                var user = new User
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = BC.HashPassword(model.Password),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    RoleId = model.RoleId,

                };

                await _dataContext.Users.AddAsync(user);
                return user;
            }

            IActionResult GetView()
            {
                var model = _dataContext.Roles.Select(r => new RoleViewModel(r.Id, r.Name)).ToList();
                return View(model);
            }

            return RedirectToRoute("admin-user-list");
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-user-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId,
                Roles = await _dataContext.Roles.Select(r => new RoleViewModel(r.Id, r.Name)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-user-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _dataContext.Users.FirstOrDefaultAsync(a => a.Id == id);
            if (user is null) return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.RoleId = model.RoleId;
            user.UpdatedAt = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-user-list");
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-user-delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null) return NotFound();

            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-user-list");
        }

        #endregion
    }
}
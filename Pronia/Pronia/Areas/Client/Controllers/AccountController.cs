using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.Account;
using Pronia.Database;
using Pronia.Services.Abstracts;
using BC = BCrypt.Net.BCrypt;

namespace Pronia.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AccountController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        #region Account

        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public async Task<IActionResult> DashboardAsync()
        {
            return View();
        }

        #endregion

        #region Order

        [HttpGet("order", Name = "client-order-dashboard")]
        public async Task<IActionResult> OrderAsync()
        {
            var model = await _dataContext.Orders.Include(o => o.OrderProducts)
               .Where(o => o.UserId == _userService.CurrentUser.Id)
               .Select(o => new OrderViewModel(o.Id, o.Total, o.OrderProducts.Count, o.Status, o.CreatedAt))
               .ToListAsync();

            return View(model);
        }

        #endregion

        #region Address

        [HttpGet("address", Name = "client-address-dashboard")]
        public async Task<IActionResult> AddressAsync()
        {
            return View();
        }

        #endregion

        #region Account Details

        [HttpGet("accountdetails", Name = "client-accountdetails-dashboard")]
        public async Task<IActionResult> AccountDetailsAsync()
        {
            var model = await _dataContext.Orders.Include(o => o.OrderProducts)
                 .Where(o => o.UserId == _userService.CurrentUser.Id)
                 .Select(o => new OrderViewModel(o.Id, o.Total, o.OrderProducts.Count, o.Status, o.CreatedAt))
                 .ToListAsync();

            return View(model);
        }

        #endregion

        #region Details

        [HttpGet("detail", Name = "client-account-detail")]
        public async Task<IActionResult> DetailsAsync()
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == _userService.CurrentUser.Id);

            var model = new UserViewModel
            {
                Email = user.Email,
                CurrentPasword = null,
                Password = null,
                ConfirmPassword = null,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(model);
        }

        [HttpPost("detail", Name = "client-account-detail")]
        public async Task<IActionResult> DetailsAsync(UserViewModel newuser)
        {
            if (!ModelState.IsValid) return View(newuser);

            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == _userService.CurrentUser.Id);

            if (user is null) return NotFound();
   
            if (newuser.CurrentPasword == user.Password) return View(newuser);

            user.FirstName = newuser.FirstName;
            user.LastName = newuser.LastName;
            user.Email = newuser.Email;
            user.Password = BC.HashPassword(newuser.Password);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-dashboard");
        }

        #endregion

        #region Logout

        [HttpGet("logout", Name = "client-logout-dashboard")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("client-home-index");
        }

        #endregion
    }
}
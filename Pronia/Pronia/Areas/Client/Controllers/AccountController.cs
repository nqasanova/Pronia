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
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AccountController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public async Task<IActionResult> DashboardAsync()
        {
            return View();
        }

        [HttpGet("order", Name = "client-order-dashboard")]
        public async Task<IActionResult> OrderAsync()
        {
            var model = await _dataContext.Orders.Include(o => o.OrderProducts)
               .Where(o => o.UserId == _userService.CurrentUser.Id)
               .Select(o => new OrderViewModel(o.Id, o.Total, o.OrderProducts.Count, o.Status, o.CreatedAt))
               .ToListAsync();

            return View(model);
        }

        //[HttpGet("orderview/{id}", Name = "client-orderview-dashboard")]
        //public async Task<IActionResult> OrderViewAsync(int id)
        //{
        //    var product = await _dataContext.OrderProducts.Include(p => p.Product)
        //       .FirstOrDefaultAsync(p => p.Id == id);

        //    if (product is null) return NotFound();
        //}

        [HttpGet("adress", Name = "client-adress-dashboard")]
        public async Task<IActionResult> AdressAsync()
        {
            return View();
        }

        [HttpGet("accountdetails", Name = "client-accountdetails-dashboard")]
        public async Task<IActionResult> AccountDetailsAsync()
        { 
            var model = await _dataContext.Orders.Include(o => o.OrderProducts)
                 .Where(o => o.UserId == _userService.CurrentUser.Id)
                 .Select(o => new OrderViewModel(o.Id, o.Total, o.OrderProducts.Count, o.Status, o.CreatedAt))
                 .ToListAsync();

            return View(model);
        }


        [HttpGet("detail", Name = "client-account-detail")]
        public async Task<IActionResult> DetailsAsync()
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == _userService.CurrentUser.Id);

            var model = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CurrentPasword = null,
                Password = null,
                ConfirmPassword = null,
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

        [HttpGet("logout", Name = "client-logout-dashboard")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("client-home-index");
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.Authentication;
using Pronia.Database;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/auth")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AuthenticationController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        #region Login and Logout

        [HttpGet("login", Name = "admin-auth-login")]
        public async Task<IActionResult> LoginAsync()
        {
            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "admin-auth-login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel? model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password is not correct");
                return View(model);
            }

            var user = await _dataContext.Users
                .Include(u => u.Role)
                .SingleAsync(u => u.Email == model!.Email);

            if (user.RoleId == 1)
            {
                await _userService.SignInAsync(model.Email, model.Password, user.Role.Name);
            }

            else
            {
                ModelState.AddModelError(String.Empty, "You are not admin");
                return View(model);
            }

            await _userService.SignInAsync(model.Email, model.Password, user.Role.Name);

            return RedirectToRoute("admin-dashboard-index");
        }

        [HttpGet("logout", Name = "admin-auth-logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("admin-auth-login");
        }

        #endregion
    }
}
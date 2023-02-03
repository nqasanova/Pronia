using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pronia.Database;
using Pronia.Areas.Admin.ViewModels.Contact;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/contact")]
    [Authorize(Roles = "admin")]
    public class ContactController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<CategoryController> _logger;

        public ContactController(DataContext dataContext, ILogger<CategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List

        [HttpGet("list", Name = "admin-contact-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Contacts
                .Select(c => new ListItemViewModel(c.FirstName, c.LastName, c.Phone, c.Email, c.Message)).ToListAsync();

            return View(model);
        }

        #endregion
    }
}
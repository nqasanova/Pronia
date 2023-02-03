using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Contracts.Email;
using Pronia.Database;
using Pronia.Services.Abstracts;
using Pronia.Areas.Admin.ViewModels.Order;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/order")]
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;

        public OrderController(DataContext dataContext, IFileService fileService, IEmailService emailService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _emailService = emailService;
        }

        #region List

        [HttpGet("list", Name = "admin-order-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Orders.Select(u => new ListItemViewModel(u.Id, u.Total, u.Status, u.CreatedAt, u.UpdatedAt)).ToListAsync();

            return View(model);
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(n => n.Id == id);

            if (order is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = id,
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateViewModel model)
        {
            var order = await _dataContext.Orders.Include(u => u.User).Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }

            order.Status = model.Status;

            var stausMessageDto = PrepareStausMessage(order.User.Email);
            _emailService.Send(stausMessageDto);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-order-list");

            MessageDto PrepareStausMessage(string email)
            {
                string body = "Order Has Been Updated";

                string subject = EmailMessages.Subject.ORDER_ACTIVATION_MESSAGE;

                return new MessageDto(email, subject, body);
            }
        }

        #endregion
    }
}
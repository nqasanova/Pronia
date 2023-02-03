using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.PaymentBenefits;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/payment")]
    [Authorize(Roles = "admin")]
    public class PaymentController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PaymentController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-payment-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.PaymentBenefits
                .Select(u => new ListItemViewModel(
                  u.Id, u.Title, u.Content, _fileService.GetFileUrl(u.ImageNameInFileSystem, UploadDirectory.PaymentBenefit), u.CreatedAt, u.UpdatedAt))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-payment-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-payment-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.PaymentBenefit);

            await AddPayment(model.Image!.FileName, imageNameInSystem);

            return RedirectToRoute("admin-payment-list");

            async Task AddPayment(string image, string imageInSystem)
            {
                var payment = new PaymentBenefit
                {
                    Title = model.Title,
                    Content = model.Content,
                    ImageName = image,
                    ImageNameInFileSystem = imageInSystem,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                await _dataContext.PaymentBenefits.AddAsync(payment);
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-payment-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var paymentBenefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(pb => pb.Id == id);
            if (paymentBenefit is null) return NotFound();

            var model = new AddViewModel
            {
                Id = paymentBenefit.Id,
                Title = paymentBenefit.Title,
                Content = paymentBenefit.Content,
                ImageURL = _fileService.GetFileUrl(paymentBenefit.ImageNameInFileSystem, UploadDirectory.PaymentBenefit)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-payment-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            var paymentBenefit = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(pb => pb.Id == model.Id);
            if (paymentBenefit is null) return NotFound();

            if (!ModelState.IsValid) return View(model);

            if (model.Image != null)
            {
                await _fileService.DeleteAsync(paymentBenefit.ImageNameInFileSystem, UploadDirectory.PaymentBenefit);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.PaymentBenefit);
                await UpdatePaymentAsync(model.Image.FileName, imageFileNameInSystem);
            }

            else
            {
                await UpdatePaymentAsync(paymentBenefit.ImageName, paymentBenefit.ImageNameInFileSystem);
            }

            return RedirectToRoute("admin-payment-list");

            async Task UpdatePaymentAsync(string image, string imageInFileSystem)
            {
                paymentBenefit.Title = model.Title;
                paymentBenefit.Content = model.Content;
                paymentBenefit.ImageName = image;
                paymentBenefit.ImageNameInFileSystem = imageInFileSystem;

                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-payment-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var payment = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(pb => pb.Id == id);
            if (payment is null) return NotFound();

            await _fileService.DeleteAsync(payment.ImageNameInFileSystem, UploadDirectory.PaymentBenefit);

            _dataContext.PaymentBenefits.Remove(payment);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-payment-list");
        }

        #endregion
    }
}
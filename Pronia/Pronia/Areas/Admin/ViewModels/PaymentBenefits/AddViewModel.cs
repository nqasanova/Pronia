using System;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Admin.ViewModels.PaymentBenefits
{
    public class AddViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }
    }
}
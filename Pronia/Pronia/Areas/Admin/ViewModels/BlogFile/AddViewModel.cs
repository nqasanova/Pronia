using System;
namespace Pronia.Areas.Admin.ViewModels.BlogFile
{
    public class AddViewModel
    {
        public IFormFile File { get; set; }

        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
    }
}
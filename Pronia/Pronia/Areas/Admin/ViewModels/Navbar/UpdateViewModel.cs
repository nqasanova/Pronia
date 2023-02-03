using System;
using Pronia.Areas.Admin.ViewModels.Product.Add;

namespace Pronia.Areas.Admin.ViewModels.Navbar
{
    public class UpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }

        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }
    }
}
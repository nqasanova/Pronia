using System;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Admin.ViewModels.Navbar
{
    public class AddViewModel
    { 
        public string Name { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }

        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }
    }
}
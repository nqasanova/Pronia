using System;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Areas.Admin.ViewModels.SubNavbar
{
    public class AddViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? URL { get; set; }
        public List<URLViewModel>? URLs { get; set; }

        [Required]
        public int Order { get; set; }

        public int NavbarId { get; set; }
        public List<NavbarViewModel>? Navbars { get; set; }

        public class URLViewModel
        {
            public string? Path { get; set; }

            public URLViewModel(string? path)
            {
                Path = path;
            }
        }

        public class NavbarViewModel
        {
            public NavbarViewModel(int id, string title)
            {
                Id = id;
                Title = title;
            }

            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}
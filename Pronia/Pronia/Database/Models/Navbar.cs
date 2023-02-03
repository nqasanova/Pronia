using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class Navbar : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }

        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<SubNavbar>? SubNavbars { get; set; }
    }
}
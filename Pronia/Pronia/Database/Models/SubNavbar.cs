using System;
using Pronia.Database.Models.Common;

namespace Pronia.Database.Models
{
    public class SubNavbar : BaseEntity<int>, IAuditable
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }

        public int NavbarId { get; set; }
        public Navbar Navbar { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
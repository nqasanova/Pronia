using System;

namespace Pronia.Areas.Admin.ViewModels.Navbar
{
    public class ListItemViewModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }

        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }

        public ListItemViewModel(int id, string name, string uRL, int order, bool isHeader, bool isFooter)
        {
            Id = id;
            Name = name;
            URL = uRL;
            Order = order;
            IsHeader = isHeader;
            IsFooter = isFooter;
        }

    }
}
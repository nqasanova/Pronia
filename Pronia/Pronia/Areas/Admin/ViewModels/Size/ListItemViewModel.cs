using System;

namespace Pronia.Areas.Admin.ViewModels.Size
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ListItemViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
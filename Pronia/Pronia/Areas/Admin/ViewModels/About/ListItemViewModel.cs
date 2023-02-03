using System;
namespace Pronia.Areas.Admin.ViewModels.About
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public ListItemViewModel(int id, string content)
        {
            Id = id;
            Content = content;
        }
    }
}
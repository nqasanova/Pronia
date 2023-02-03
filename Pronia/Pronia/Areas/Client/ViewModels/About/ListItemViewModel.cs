using System;

namespace Pronia.Areas.Client.ViewModels.About
{
    public class ListItemViewModel
    {
        public string Content { get; set; }

        public ListItemViewModel(string content)
        {
            Content = content;
        }
    }
}
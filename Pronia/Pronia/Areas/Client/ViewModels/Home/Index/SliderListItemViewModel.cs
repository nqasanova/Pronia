using System;
namespace Pronia.Areas.Client.ViewModels.Home.Index
{
    public class SliderListItemViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ButtonName { get; set; }
        public string ButtonURL { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }
        public int Order { get; set; }

        public SliderListItemViewModel(string title, string content, string buttonName, string buttonURL, string imageName, string imageURL, int order)
        {
            Title = title;
            Content = content;
            ButtonName = buttonName;
            ButtonURL = buttonURL;
            ImageName = imageName;
            ImageURL = imageURL;
            Order = order;
        }
    }
}
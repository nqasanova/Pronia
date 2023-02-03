using System;
namespace Pronia.Areas.Client.ViewModels.Home.Index
{
    public class FeedbackListItemViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string Role { get; set; }
        public string ImageURL { get; set; }

        public FeedbackListItemViewModel(int id, string fullName, string content, string role, string imageURL)
        {
            Id = id;
            FullName = fullName;
            Content = content;
            Role = role;
            ImageURL = imageURL;
        }
    }
}
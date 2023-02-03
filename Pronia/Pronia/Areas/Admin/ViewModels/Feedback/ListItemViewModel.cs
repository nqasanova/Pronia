using System;
namespace Pronia.Areas.Admin.ViewModels.Feedback
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string Role { get; set; }
        public string ImageURL { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ListItemViewModel(int id, string fullName, string content, string role, string imageURL, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            FullName = fullName;
            Content = content;
            Role = role;
            ImageURL = imageURL;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
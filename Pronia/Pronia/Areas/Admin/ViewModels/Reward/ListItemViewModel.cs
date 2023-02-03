using System;
namespace Pronia.Areas.Admin.ViewModels.Reward
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ListItemViewModel(int id, string? imageURL, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            ImageURL = imageURL;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
using System;
namespace Pronia.Areas.Client.ViewModels.Home.Index
{
    public class BlogListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileURL { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }
        public DateTime CreatedAt { get; set; }

        public BlogListItemViewModel(int id, string name, string description, string fileURL, bool isImage, bool isVideo, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            FileURL = fileURL;
            IsImage = isImage;
            IsVideo = isVideo;
            CreatedAt = createdAt;
        }
    }
}
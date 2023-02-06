using System;
namespace Pronia.Areas.Client.ViewModels.BlogPage
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FileURL { get; set; }
        public bool IsImage { get; set; }
        public bool IsVideo { get; set; }

        public List<TagViewModel> Tags { get; set; }
        public List<CategoryViewModel> Categories { get; set; }

        public ListItemViewModel(int id, string name, string description, DateTime createdAt, string fileURL, bool isImage, bool isVideo, List<TagViewModel> tags, List<CategoryViewModel> categories)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            FileURL = fileURL;
            IsImage = isImage;
            IsVideo = isVideo;
            Tags = tags;
            Categories = categories;
        }

        public class TagViewModel
        {
            public string Name { get; set; }

            public TagViewModel(string name)
            {
                Name = name;
            }
        }

        public class CategoryViewModel
        {
            public string Name { get; set; }
            public string ParentName { get; set; }

            public CategoryViewModel(string name, string parentName)
            {
                Name = name;
                ParentName = parentName;
            }
        }
    }
}
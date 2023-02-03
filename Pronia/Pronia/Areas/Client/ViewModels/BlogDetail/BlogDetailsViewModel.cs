using System;
using static Pronia.Areas.Client.ViewModels.BlogDetail.BlogDetailsViewModel;

namespace Pronia.Areas.Client.ViewModels.BlogDetail
{
    public class BlogDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<TagViewModel> Tags { get; set; }
        public List<CategoryViewModel> Catagories { get; set; }
        public List<FileViewModel> Files { get; set; }

        public class TagViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public TagViewModel(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public CategoryViewModel(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class FileViewModel
        {
            public string FileURL { get; set; }
            public bool IsImage { get; set; }
            public bool IsVideo { get; set; }

            public FileViewModel(string fileURL, bool isImage, bool isVideo)
            {
                FileURL = fileURL;
                IsImage = isImage;
                IsVideo = isVideo;
            }
        }
    }
}
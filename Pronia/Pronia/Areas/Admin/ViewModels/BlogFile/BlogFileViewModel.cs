using System;
namespace Pronia.Areas.Admin.ViewModels.BlogFile
{
    public class BlogFileViewModel
    {
        public int BlogId { get; set; }
        public List<ListItem>? Files { get; set; }


        public class ListItem
        {
            public int Id { get; set; }
            public string? FileURL { get; set; }

            public DateTime CreatedAt { get; set; }

            public bool IsImage { get; set; }
            public bool IsVideo { get; set; }
        }
    }
}
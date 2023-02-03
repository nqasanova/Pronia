using System;
namespace Pronia.Areas.Client.ViewModels.Home.Modal
{
    public class ModalViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }

        public List<SizeViewModel> Sizes { get; set; }
        public List<ColorViewModel> Colors { get; set; }

        public ModalViewModel(int id, string name, string description, decimal price, string imageURL, List<SizeViewModel> sizes, List<ColorViewModel> colors)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImageURL = imageURL;
            Sizes = sizes;
            Colors = colors;
        }

        public class SizeViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public SizeViewModel(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class ColorViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public ColorViewModel(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}
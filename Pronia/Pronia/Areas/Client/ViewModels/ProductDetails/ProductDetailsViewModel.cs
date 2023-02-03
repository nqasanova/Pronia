using System;
using Pronia.Areas.Client.ViewModels.Home.Index;
using Pronia.Areas.Client.ViewModels.Product;

namespace Pronia.Areas.Client.ViewModels.ProductDetails
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public List<TagViewModel> Tags { get; set; }
        public List<ColorViewModel> Colors { get; set; }
        public List<SizeViewModel> Sizes { get; set; }
        public List<CatagoryViewModel> Catagories { get; set; }
        public List<ImageViewModel> Images { get; set; }

        public List<ListItemViewModel> Products { get; set; }
        public List<PaymentBListItemViewModel> PaymentBenefits { get; set; }

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

        public class ImageViewModel
        {
            public string ImageURL { get; set; }

            public ImageViewModel(string imageURL)
            {
                ImageURL = imageURL;
            }
        }

        public class CatagoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public CatagoryViewModel(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}
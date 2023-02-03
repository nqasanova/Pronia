using System;
namespace Pronia.Areas.Client.ViewModels.Home.Index
{
    public class PaymentBListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }

        public PaymentBListItemViewModel(int id, string name, string content, string imageURL)
        {
            Id = id;
            Name = name;
            Content = content;
            ImageURL = imageURL;
        }
    }
}
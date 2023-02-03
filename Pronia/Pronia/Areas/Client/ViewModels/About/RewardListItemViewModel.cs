using System;
namespace Pronia.Areas.Client.ViewModels.About
{
    public class RewardListItemViewModel
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }

        public RewardListItemViewModel(int id, string imageURL)
        {
            Id = id;
            ImageURL = imageURL;
        }
    }
}


using System;
namespace Pronia.Areas.Client.ViewModels.Home.Index
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
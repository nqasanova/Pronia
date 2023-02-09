using System;

namespace Pronia.Areas.Client.ViewModels.About
{
    public class AboutViewModel
    {
        public List<ListItemViewModel> Abouts { get; set; }
        public List<PaymentBListItemViewModel> PaymentBenefits { get; set; }
    }
}
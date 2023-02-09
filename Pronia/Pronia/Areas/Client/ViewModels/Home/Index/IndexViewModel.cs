using System;
using Pronia.Areas.Admin.ViewModels.PaymentBenefits;

namespace Pronia.Areas.Client.ViewModels.Home.Index
{
    public class IndexViewModel
    {
        public List<BlogListItemViewModel> Blogs { get; set; }
        public List<ProductListItemViewModel> Products { get; set; }
        public List<SliderListItemViewModel> Sliders { get; set; }
        public List<PaymentBListItemViewModel> PaymentBenefits { get; set; }
        public List<FeedbackListItemViewModel> Feedbacks { get; set; }
    }
}
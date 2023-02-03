//using System;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Pronia.Database;

//namespace Pronia.Areas.Admin.ViewComponents
//{
//    [ViewComponent(Name = "NavbarFooter")]
//    public class NavbarFooterComponent : ViewComponent
//    {
//        private readonly DataContext _datacontext;
//        public NavbarFooterComponent(DataContext datacontext)
//        {
//            _datacontext = datacontext;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            var model = _datacontext.Navbars
//                .Include(n => n.SubNavbars
//                .OrderBy(sn => sn.Order))
//                .Where(n => n.IsFooter)
//                .OrderBy(n => n.Order)
//                .ToList();

//            return View("~/Views/Shared/Components/NavbarFooter/Index.cshtml", model);
//        }
//    }
//}
//using System;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Pronia.Database;

//namespace Pronia.Areas.Admin.ViewComponents
//{
//    public class NavbarHeaderComponent
//    {
//        [ViewComponent(Name = "NavbarHeader")]
//        public class NavbarHeaderComponents : ViewComponent
//        {
//            private readonly DataContext _datacontext;
//            public NavbarHeaderComponents(DataContext datacontext)
//            {
//                _datacontext = datacontext;
//            }

//            public async Task<IViewComponentResult> InvokeAsync()
//            {
//                var model = _datacontext.Navbars
//                    .Include(n => n.SubNavbars
//                    .OrderBy(sn => sn.Order))
//                    .Where(n => n.IsHeader)
//                    .OrderBy(n => n.Order)
//                    .ToList();

//                return View("~/Views/Shared/Components/NavbarHeader/Index.cshtml", model);
//            }
//        }
//    }
//}
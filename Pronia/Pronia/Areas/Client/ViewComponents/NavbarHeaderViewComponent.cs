using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Database;

namespace Pronia.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "NavbarHeaderViewComponent")]
    public class NavbarHeaderViewComponent : ViewComponent
    {
        private readonly DataContext _datacontext;

        public NavbarHeaderViewComponent(DataContext dataContext)
        {
            _datacontext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _datacontext.Navbars.Include(n => n.SubNavbars.OrderBy(sn => sn.Order)).Where(n => n.IsHeader).OrderBy(n => n.Order).ToListAsync();

            return View(model);
        }
    }
}
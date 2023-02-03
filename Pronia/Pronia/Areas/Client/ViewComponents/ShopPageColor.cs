using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.ShopCart;
using Pronia.Areas.Client.ViewModels.ShopPage;
using Pronia.Database;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageColor")]
    public class ShopPageColor : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ShopPageColor(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync();

            return View(model);
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.BlogPage;
using Pronia.Database;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "BlogPageTag")]
    public class BlogPageTag : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BlogPageTag(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.BlogTags.Select(c => new TagListItemViewModel(c.Id, c.Name)).ToListAsync();

            return View(model);
        }
    }
}
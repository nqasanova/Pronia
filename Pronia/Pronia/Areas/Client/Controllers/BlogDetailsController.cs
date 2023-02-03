using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Client.ViewModels.BlogDetail;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Client.Controllers
{
    [Area("client")]
    [Route("blogdetails")]
    public class BlogDetailsController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public BlogDetailsController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("index/{id}", Name = "client-blogdetails-index")]
        public async Task<IActionResult> Index(int id)
        {
            var blog = await _dbContext.Blogs.Include(p => p.BlogFiles)
                .Include(p => p.BlogandBlogTags)
                .Include(p => p.BlogandBlogCategories).FirstOrDefaultAsync(p => p.Id == id);

            if (blog is null) return NotFound();

            var model = new BlogDetailsViewModel
            {
                Id = blog.Id,
                Name = blog.Name,
                Description = blog.Description,

                Tags = _dbContext.BlogandBlogTags.Include(ps => ps.Tag).Where(ps => ps.BlogId == blog.Id)
                      .Select(ps => new BlogDetailsViewModel.TagViewModel(ps.Tag.Id, ps.Tag.Name)).ToList(),

                Catagories = _dbContext.BlogandBlogCategories.Include(ps => ps.Category).Where(ps => ps.BlogId == blog.Id)
                         .Select(ps => new BlogDetailsViewModel.CategoryViewModel(ps.Category.Id, ps.Category.Name)).ToList(),

                Files = _dbContext.BlogFiles.Where(p => p.BlogId == blog.Id)
                .Select(p => new BlogDetailsViewModel.FileViewModel
                (_fileService.GetFileUrl(p.FileNameInFileSystem, UploadDirectory.Blog), p.IsImage, p.IsVideo)).ToList()
            };

            return View(model);
        }
    }
}
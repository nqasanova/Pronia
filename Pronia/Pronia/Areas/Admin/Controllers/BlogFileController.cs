using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.BlogFile;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Abstracts;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogfile")]
    [Authorize(Roles = "admin")]
    public class BlogFileController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BlogFileController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("{blogId}/image/list", Name = "admin-blogfile-list")]
        public async Task<IActionResult> ListAsync([FromRoute] int blogId)
        {
            var blog = await _dataContext.Blogs.Include(b => b.BlogFiles).FirstOrDefaultAsync(b => b.Id == blogId);

            if (blog == null) return NotFound();

            var model = new BlogFileViewModel { BlogId = blog.Id };

            model.Files = blog.BlogFiles.Select(b => new BlogFileViewModel.ListItem
            {
                Id = b.Id,
                FileURL = _fileService.GetFileUrl(b.FileNameInFileSystem, Contracts.File.UploadDirectory.Blog),
                CreatedAt = b.CreatedAt,
                IsImage = b.IsImage,
                IsVideo = b.IsVideo,
            }).ToList();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("{blogId}/image/add", Name = "admin-blogfile-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{blogId}/image/add", Name = "admin-blogfile-add")]
        public async Task<IActionResult> AddAsync([FromRoute] int blogId, AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var blog = await _dataContext.Blogs.FirstOrDefaultAsync(b => b.Id == blogId);

            if (blog is null)
            {
                return NotFound();
            }

            var fileNameInSystem = await _fileService.UploadAsync(model.File, UploadDirectory.Blog);

            var blogFile = new BlogFile
            {
                Blog = blog,
                FileName = model.File.FileName,
                FileNameInFileSystem = fileNameInSystem,
                IsImage = model.IsImage,
                IsVideo = model.IsVideo,
            };

            await _dataContext.BlogFiles.AddAsync(blogFile);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogfile-list", new { BlogId = blogId });
        }
        #endregion

        #region Delete

        [HttpPost("{blogId}/image/{blogFileId}/delete", Name = "admin-blogfile-delete")]
        public async Task<IActionResult> Delete([FromRoute] int blogId, [FromRoute] int blogFileId)
        {
            var blogFile = await _dataContext.BlogFiles.FirstOrDefaultAsync(bf => bf.BlogId == blogId && bf.Id == blogFileId);

            if (blogFile is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(blogFile.FileNameInFileSystem, UploadDirectory.Blog);

            _dataContext.BlogFiles.Remove(blogFile);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogfile-list", new { BlogId = blogId });
        }

        #endregion
    }
}
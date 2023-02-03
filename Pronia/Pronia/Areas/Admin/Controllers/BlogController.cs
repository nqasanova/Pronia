using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.Blog;
using Pronia.Database;
using Pronia.Database.Models;
using static Pronia.Areas.Admin.ViewModels.Blog.AddViewModel;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blog")]
    [Authorize(Roles = "admin")]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ProductController> _logger;

        public BlogController(DataContext dataContext, ILogger<ProductController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;

        }

        #region List

        [HttpGet("list", Name = "admin-blog-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Blogs.Select(b => new ListItemViewModel(b.Id, b.Name, b.Description,
                b.CreatedAt,
                b.BlogandBlogTags.Select(bt => bt.Tag).Select(s => new ListItemViewModel.TagViewModel(s.Name)).ToList(),
                b.BlogandBlogCategories.Select(bc => bc.Category).Select(c => new ListItemViewModel.CategoryViewModel(c.Name, c.Parent.Name)).ToList())).ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-blog-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Tags = await _dataContext.BlogTags.Select(t => new TagListItemViewModel(t.Id, t.Name)).ToListAsync(),

                Categories = await _dataContext.BlogCategories.Select(c => new CategoryListItemViewModel(c.Id, c.Name)).ToListAsync(),
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-blog-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.BlogTags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) could not be found... ");
                    return GetView(model);
                }
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.BlogCategories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) could not be found...");
                    return GetView(model);
                }
            }

            AddBlog();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");

            IActionResult GetView(AddViewModel model)
            {
                model.Tags = _dataContext.BlogTags.Select(c => new TagListItemViewModel(c.Id, c.Name)).ToList();

                model.Categories = _dataContext.BlogCategories.Select(c => new CategoryListItemViewModel(c.Id, c.Name)).ToList();

                return View(model);
            }

            async void AddBlog()
            {
                var blog = new Blog
                {
                    Name = model.Name,
                    Description = model.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _dataContext.Blogs.AddAsync(blog);

                foreach (var tagId in model.TagIds)
                {
                    var blogTag = new BlogandBlogTag
                    {
                        BlogTagId = tagId,
                        Blog = blog,
                    };

                    await _dataContext.BlogandBlogTags.AddAsync(blogTag);
                }

                foreach (var catagoryId in model.CategoryIds)
                {
                    var BlogCatagory = new BlogandBlogCategory
                    {
                        BlogCategoryId = catagoryId,
                        Blog = blog,
                    };

                    await _dataContext.BlogandBlogCategories.AddAsync(BlogCatagory);
                }
            }
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var blog = await _dataContext.Blogs
                .Include(t => t.BlogandBlogTags)
                .Include(c => c.BlogandBlogCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (blog is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = blog.Id,
                Name = blog.Name,
                Description = blog.Description,
                Tags = await _dataContext.BlogTags.Select(c => new TagListItemViewModel(c.Id, c.Name)).ToListAsync(),
                TagIds = blog.BlogandBlogTags.Select(pc => pc.BlogTagId).ToList(),

                Categories = await _dataContext.BlogCategories.Select(c => new CategoryListItemViewModel(c.Id, c.Name)).ToListAsync(),
                CategoryIds = blog.BlogandBlogCategories.Select(pc => pc.BlogCategoryId).ToList(),
            };

            return View(model);

        }

        [HttpPost("update/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var blog = await _dataContext.Blogs
                .Include(t => t.BlogandBlogTags)
                    .Include(c => c.BlogandBlogCategories)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (blog is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.BlogTags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) could not be found... ");
                    return GetView(model);
                }
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.BlogCategories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong...");
                    _logger.LogWarning($"Category with id({categoryId}) could not be found... ");
                    return GetView(model);
                }
            }

            UpdateBlogAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");


            IActionResult GetView(UpdateViewModel model)
            {
                model.Tags = _dataContext.BlogTags.Select(c => new TagListItemViewModel(c.Id, c.Name)).ToList();
                model.TagIds = blog.BlogandBlogTags.Select(c => c.BlogTagId).ToList();

                model.Categories = _dataContext.BlogCategories.Select(c => new CategoryListItemViewModel(c.Id, c.Name)).ToList();
                model.CategoryIds = blog.BlogandBlogCategories.Select(c => c.BlogCategoryId).ToList();

                return View(model);
            }

            async Task UpdateBlogAsync()
            {
                blog.Name = model.Name;
                blog.Description = model.Description;
                blog.UpdatedAt = DateTime.Now;

                #region Tag

                var tagInDb = blog.BlogandBlogTags.Select(bc => bc.BlogTagId).ToList();
                var tagToAdd = model.TagIds.Except(tagInDb).ToList();
                var tagToRemove = tagInDb.Except(model.TagIds).ToList();

                blog.BlogandBlogTags.RemoveAll(bc => tagToRemove.Contains(bc.BlogTagId));


                foreach (var tagId in tagToAdd)
                {
                    var BlogTag = new BlogandBlogTag
                    {
                        BlogTagId = tagId,
                        Blog = blog,
                    };

                    await _dataContext.BlogandBlogTags.AddAsync(BlogTag);
                }

                #endregion

                #region Catagory

                var categoriesInDb = blog.BlogandBlogCategories.Select(bc => bc.BlogCategoryId).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();
                var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();

                blog.BlogandBlogCategories.RemoveAll(bc => categoriesToRemove.Contains(bc.BlogCategoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var productCategory = new BlogandBlogCategory
                    {
                        BlogCategoryId = categoryId,
                        Blog = blog,
                    };

                    await _dataContext.BlogandBlogCategories.AddAsync(productCategory);
                }

                #endregion
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-blog-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var blog = await _dataContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);

            if (blog is null)
            {
                return NotFound();
            }

            _dataContext.Blogs.Remove(blog);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");
        }

        #endregion
    }
}
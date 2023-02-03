using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Areas.Admin.ViewModels.Product;
using Pronia.Areas.Admin.ViewModels.Product.Add;
using Pronia.Database;
using Pronia.Database.Models;
using System.Linq;
using static Pronia.Areas.Admin.ViewModels.Product.ListItemViewModel;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/Product")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ProductController> _logger;

        public ProductController(DataContext dataContext, ILogger<ProductController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List

        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Products.Select(p => new ListItemViewModel(p.Id, p.Name, p.Content, p.Price, p.CreatedAt,
                p.ProductTags.Select(pt => pt.Tag).Select(t => new TagViewModel(t.Name)).ToList(),
                p.ProductSizes.Select(ps => ps.Size).Select(s => new SizeViewModel(s.Name)).ToList(),
                p.ProductColors.Select(pc => pc.Color).Select(c => new ColorViewModel(c.Name)).ToList(),
                p.ProductCategories.Select(pc => pc.Category).Select(c => new CategoryViewModel(c.Name, c.Parent.Name)).ToList())).ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Tags = await _dataContext.Tags.Select(t => new TagListItemViewModel(t.Id, t.Name)).ToListAsync(),
                Sizes = await _dataContext.Sizes.Select(s => new SizeListItemViewModel(s.Id, s.Name)).ToListAsync(),
                Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                Categories = await _dataContext.Categories.Select(c => new CategoryListItemViewModel(c.Id, c.Name)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            foreach (var id in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(t => t.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "Something went wrong...");
                    return GetView(model);
                }
            }

            foreach (var id in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(s => s.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "Something went wrong...");
                    return GetView(model);
                }
            }

            foreach (var id in model.ColorIds)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "Something went wrong...");
                    return GetView(model);
                }
            }

            foreach (var id in model.CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "Something went wrong...");
                    return GetView(model);
                }
            }

            AddProduct();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");

            IActionResult GetView(AddViewModel model)
            {
                model.Tags = _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.Name)).ToList();

                model.Sizes = _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.Name)).ToList();

                model.Colors = _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToList();

                model.Categories = _dataContext.Categories.Select(c => new CategoryListItemViewModel(c.Id, c.Name)).ToList();

                return View(model);
            }

            async void AddProduct()
            {
                var product = new Product
                {
                    Name = model.Name,
                    Content = model.Description,
                    Price = model.Price,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                await _dataContext.Products.AddAsync(product);

                foreach (var tagId in model.TagIds)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }

                foreach (var sizeId in model.SizeIds)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

                foreach (var colorId in model.ColorIds)
                {
                    var productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product,
                    };

                    await _dataContext.ProductColors.AddAsync(productColor);
                }

                foreach (var categoryId in model.CategoryIds)
                {
                    var productCatagory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCategories.AddAsync(productCatagory);
                }
            }
        }
        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products
                .Include(c => c.ProductTags)
                .Include(c => c.ProductSizes)
                .Include(s => s.ProductColors)
                .Include(t => t.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null) return NotFound();

            var newModel = new UpdateViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Content,
                Price = product.Price,

                Tags = await _dataContext.Tags.Select(t=> new TagListItemViewModel(t.Id, t.Name)).ToListAsync(),
                TagIds = product.ProductTags.Select(pt => pt.TagId).ToList(),

                Sizes = await _dataContext.Sizes.Select(s => new SizeListItemViewModel(s.Id, s.Name)).ToListAsync(),
                SizeIds = product.ProductSizes.Select(ps => ps.SizeId).ToList(),

                Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                ColorIds = product.ProductColors.Select(pc => pc.ColorId).ToList(),

                Categories = await _dataContext.Categories.Select(c => new CategoryListItemViewModel(c.Id, c.Name)).ToListAsync(),
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
            };

            return View(newModel);

        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var product = await _dataContext.Products
                    .Include(c => c.ProductTags)
                    .Include(c => c.ProductSizes)
                    .Include(s => s.ProductColors)
                    .Include(t => t.ProductCategories)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product is null) return NotFound();

            if (!ModelState.IsValid) return GetView(model);


            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong!");
                    _logger.LogWarning($"Tag with id({tagId}) could not be found... ");
                    return GetView(model);
                }
            }

            foreach (var sizeId in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong!");
                    _logger.LogWarning($"Size with id({sizeId}) could not be found... ");
                    return GetView(model);
                }
            }

            foreach (var colorId in model.ColorIds)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong!");
                    _logger.LogWarning($"Color with id({colorId}) could not be found... ");
                    return GetView(model);
                }
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong!");
                    _logger.LogWarning($"Category with id({categoryId}) could not be found... ");
                    return GetView(model);
                }
            }

            UpdateProductAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");


            IActionResult GetView(UpdateViewModel model)
            {
                model.Tags = _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.Name)).ToList();
                model.TagIds = product.ProductTags.Select(c => c.TagId).ToList();

                model.Sizes = _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.Name)).ToList();
                model.SizeIds = product.ProductSizes.Select(c => c.SizeId).ToList();

                model.Colors = _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToList();
                model.ColorIds = product.ProductColors.Select(c => c.ColorId).ToList();

                model.Categories = _dataContext.Categories.Select(c => new CategoryListItemViewModel(c.Id, c.Name)).ToList();
                model.CategoryIds = product.ProductCategories.Select(c => c.CategoryId).ToList();

                return View(model);
            }

            async Task UpdateProductAsync()
            {
                product.Name = model.Name;
                product.Content = model.Description;
                product.Price = model.Price;
                product.UpdatedAt = DateTime.Now;

                #region Tag

                var tagInDb = product.ProductTags.Select(pt => pt.TagId).ToList();
                var tagToAdd = model.TagIds.Except(tagInDb).ToList();
                var tagToRemove = tagInDb.Except(model.TagIds).ToList();

                product.ProductTags.RemoveAll(pt => tagToRemove.Contains(pt.TagId));


                foreach (var tagId in tagToAdd)
                {
                    var productTag = new ProductTag
                    {
                        TagId = tagId,
                        Product = product,
                    };

                    await _dataContext.ProductTags.AddAsync(productTag);
                }

                #endregion

                #region Size

                var sizeInDb = product.ProductSizes.Select(ps => ps.SizeId).ToList();
                var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();
                var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();

                product.ProductSizes.RemoveAll(ps => sizeToRemove.Contains(ps.SizeId));

                foreach (var sizeId in sizeToAdd)
                {
                    var productSize = new ProductSize
                    {
                        SizeId = sizeId,
                        Product = product,
                    };

                    await _dataContext.ProductSizes.AddAsync(productSize);
                }

                #endregion

                #region Color

                var colorInDb = product.ProductColors.Select(pc => pc.ColorId).ToList();
                var colorToAdd = model.ColorIds.Except(colorInDb).ToList();
                var colorToRemove = colorInDb.Except(model.ColorIds).ToList();

                product.ProductColors.RemoveAll(pc => colorToRemove.Contains(pc.ColorId));


                foreach (var colorId in colorToAdd)
                {
                    var productColor = new ProductColor
                    {
                        ColorId = colorId,
                        Product = product,
                    };

                    await _dataContext.ProductColors.AddAsync(productColor);
                }
                #endregion

                #region Catagory

                var categoriesInDb = product.ProductCategories.Select(bc => bc.CategoryId).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();
                var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();

                product.ProductCategories.RemoveAll(bc => categoriesToRemove.Contains(bc.CategoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var productCategory = new ProductCategory
                    {
                        CategoryId = categoryId,
                        Product = product,
                    };

                    await _dataContext.ProductCategories.AddAsync(productCategory);
                }
                #endregion
            }
        }
        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null) return NotFound();

            _dataContext.Products.Remove(product);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");
        }

        #endregion
    }
}
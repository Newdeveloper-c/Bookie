using Bookie.Models.Entities;
using Bookie.Models.ViewModels;
using Bookie.Utilities;
using Bookie.Web.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookie.Web.Areas.Admin.Controllers;

[Area("admin")]
[Authorize(Roles = StaticDetails.Role_Admin)]
public class ProductController : Controller
{
    private readonly IProductService _productService;
    private IWebHostEnvironment _webHostEnvironment;

    public ProductController(
        IProductService productService, 
        IWebHostEnvironment webHostEnvironment)
    {
        _productService = productService;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsAsync();
        return View(products);
    }

    public async Task<IActionResult> Upsert(int Id = 0)
    {
        var productVM = new ProductViewModel
            {
                CategoryList = await _productService.GetAllCategoriesAsync(),
                Product = new Product()
            };

        if (Id != 0)
            productVM.Product = await _productService.GetProductAsync(Id);

        return View(productVM);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(ProductViewModel productVM, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            TempData["error"] = "Something went wrong. Please try again !!!";
            productVM.CategoryList = await _productService.GetAllCategoriesAsync();
            return View(productVM);
        }

        if(file != null)
        {
            var extentsion = Path.GetExtension(file.FileName);
            if(".jpg .jpeg .png .bmt".Contains(extentsion))
            {
                var root = _webHostEnvironment.WebRootPath;
                var fileName = Guid.NewGuid().ToString() + extentsion;
                var filePath = Path.Combine(root, @"images\product");

                var oldImage = root + productVM.Product.ImageUrl;
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }

                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productVM.Product.ImageUrl = @"\images\product\" + fileName;
                await _productService.UpdateProductAsync(productVM.Product);
            } 
        }
        
        if(productVM.Product.Id == 0)
        {
            await _productService.AddProductAsync(productVM.Product);
            TempData["success"] = "Product created successfully";
        }
        else
        {
            await _productService.UpdateProductAsync(productVM.Product);
            TempData["success"] = "Product updated successfully";
        }
        
        return RedirectToAction("Index");
    }

    //public async Task<IActionResult> Delete(int? id)
    //{
    //    if (id == null || id <= 0)
    //        return NotFound();

    //    var product = await _productService.GetProductAsync(id);

    //    if (product is null)
    //        NotFound();

    //    return View(new ProductViewModel
    //    {
    //        Product = product,
    //        CategoryList = new List<SelectListItem> { await _productService.GetCategoryAsync(product.CategoryId) }
    //    }) ;
    //}

    //[HttpPost, ActionName("Delete")]
    //public async Task<IActionResult> DeletePost(ProductViewModel productVM)
    //{
    //    await _productService.DeleteProductAsync(productVM.Product);
    //    TempData["success"] = "Product deleted successfully";
    //    return RedirectToAction("Index");
    //}

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Json(new { data = products });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductAsync(id);
        if(product is null)
            return Json(new { success = false, message = "Error while deleting"});

        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.Trim('\\'));

        if(System.IO.File.Exists(oldImagePath))
            System.IO.File.Delete(oldImagePath);

        await _productService.DeleteProductAsync(product);
        return Json(new { success = true, message = "Product successfully deleted" });
    }
    #endregion
}

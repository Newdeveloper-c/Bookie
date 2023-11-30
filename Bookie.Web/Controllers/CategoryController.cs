using Bookie.Models.Entities;
using Bookie.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookie.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
            ModelState.AddModelError("name", "Category Name and Display Order can not be same.");
        if (category.Name?.ToLower() == "test")
            ModelState.AddModelError("", "Test is invalid value for Category Name.");
        if (! ModelState.IsValid)
        {
            return View();
        }

        var createResult = await _categoryService.AddCategoryAsync(category);
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if(id == null || id <= 0)
            return NotFound();
        
        var category = await _categoryService.GetCategoryAsync(id);

        if(category is null)
            NotFound();

        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
            ModelState.AddModelError("name", "Category Name and Display Order can not be same.");
        if (category.Name?.ToLower() == "test")
            ModelState.AddModelError("", "Test is invalid value for Category Name.");
        if (!ModelState.IsValid)
        {
            return View();
        }

        await _categoryService.UpdateCategoryAsync(category);
        TempData["success"] = "Category updated successfully";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id <= 0)
            return NotFound();

        var category = await _categoryService.GetCategoryAsync(id);

        if (category is null)
            NotFound();

        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}

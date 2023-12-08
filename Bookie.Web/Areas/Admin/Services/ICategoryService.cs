using Bookie.Models.Entities;

namespace Bookie.Web.Areas.Admin.Services;

public interface ICategoryService
{
    Task AddCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task UpdateCategoryAsync(Category category);
    Task<Category?> GetCategoryAsync(int? categoryId);
}

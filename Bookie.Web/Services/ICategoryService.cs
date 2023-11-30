using Bookie.Models.Entities;

namespace Bookie.Web.Services;

public interface ICategoryService
{
    Task<Category> AddCategoryAsync(Category category);
    Task DeleteCategoryAsync(int categoryId);
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task UpdateCategoryAsync(Category category);
    Task<Category?> GetCategoryAsync(int? categoryId);
}

using Bookie.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookie.Web.Areas.Admin.Services;

public interface IProductService
{
    Task AddProductAsync(Product product);
    Task DeleteProductAsync(Product product);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task UpdateProductAsync(Product product);
    Task<Product?> GetProductAsync(int? productId);
    Task<IEnumerable<SelectListItem>> GetAllCategoriesAsync();
    Task<SelectListItem> GetCategoryAsync(int categoryId);
}

using Bookie.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookie.Web.Areas.Customer.Services;

public interface ICustomerService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductAsync(int? productId);
    Task<IEnumerable<SelectListItem>> GetAllCategoriesAsync();
    Task<SelectListItem> GetCategoryAsync(int categoryId);
}

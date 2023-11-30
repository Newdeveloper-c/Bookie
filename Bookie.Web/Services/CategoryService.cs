using Bookie.DataAccess.Context;
using Bookie.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookie.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _dbContext;

    public CategoryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        var addResult = await _dbContext.Categories.AddAsync(category);

        await _dbContext.SaveChangesAsync();

        return addResult.Entity;
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        => await _dbContext.Categories.ToListAsync()
            ?? Enumerable.Empty<Category>();
    
    public async Task UpdateCategoryAsync(Category category)
    {
        _dbContext.Categories.Update(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Category?> GetCategoryAsync(int? categoryId) 
        => await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
}

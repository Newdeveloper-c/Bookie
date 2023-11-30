using Bookie.DataAccess.Repository.IRepository;
using Bookie.Models.Entities;

namespace Bookie.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _unitOfWork.Category.AddAsync(category);

        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        _unitOfWork.Category.Remove(category);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        => await _unitOfWork.Category.GetAllAsync()
            ?? Enumerable.Empty<Category>();
    
    public async Task UpdateCategoryAsync(Category category)
    {
        _unitOfWork.Category.Update(category);
        await _unitOfWork.SaveAsync();
    }

    public async Task<Category?> GetCategoryAsync(int? categoryId) 
        => await _unitOfWork.Category.GetAsync(c => c.Id == categoryId);
}

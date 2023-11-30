using Bookie.DataAccess.Context;
using Bookie.DataAccess.Repository.IRepository;
using Bookie.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookie.DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDbContext _dbContext;
    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _dbContext = appDbContext;
    }

    public void Update(Category category)
    {
        _dbContext.Categories.Update(category);
    }
}

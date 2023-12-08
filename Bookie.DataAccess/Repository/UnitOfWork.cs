using Bookie.DataAccess.Context;
using Bookie.DataAccess.Repository.IRepository;

namespace Bookie.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;
    public ICategoryRepository Category {  get; private set; }
    public IProductRepository Product { get; private set; }

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        Category = new CategoryRepository(_appDbContext);
        Product = new ProductRepository(_appDbContext);
    }

    public async Task SaveAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }
}

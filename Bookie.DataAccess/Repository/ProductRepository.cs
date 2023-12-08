using Bookie.DataAccess.Context;
using Bookie.DataAccess.Repository.IRepository;
using Bookie.Models.Entities;

namespace Bookie.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _appDbContext;
    public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void Update(Product product)
    {
        _appDbContext.Products.Update(product);
    }
}

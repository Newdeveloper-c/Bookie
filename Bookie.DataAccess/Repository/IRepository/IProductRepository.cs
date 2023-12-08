
using Bookie.Models.Entities;

namespace Bookie.DataAccess.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product product);
}

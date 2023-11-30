using System.Linq.Expressions;

namespace Bookie.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}

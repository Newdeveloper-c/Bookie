using Bookie.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Bookie.DataAccess.Repository.IRepository;

namespace Bookie.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _appDbContext;
    internal DbSet<T> _dbSet;

    public Repository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _dbSet = _appDbContext.Set<T>();
    }

    public async Task AddAsync(T  entity)
        => await _dbSet.AddAsync(entity);

    public async Task<IEnumerable<T>> GetAllAsync(string? includeProp = null)
    {
        IQueryable<T> query = _dbSet;
        if(includeProp != null)
            foreach (var prop in includeProp.Split(',', StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(prop);
        
        return await query.ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, string? includeProp = null)
    {
        IQueryable<T> query = _dbSet;
        if (includeProp != null)
            foreach (var prop in includeProp.Split(',', StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(prop);

        return await query.FirstOrDefaultAsync(predicate);
    }

    public void Remove(T entity)
    => _dbSet.Remove(entity);

    public void RemoveRange(IEnumerable<T> entities)
    => _dbSet.RemoveRange(entities);
}

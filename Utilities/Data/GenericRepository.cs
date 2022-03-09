using Microsoft.EntityFrameworkCore;

namespace Utilities_aspnet.Utilities.Data;

public interface IGenericCrudRepository<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T?> Get(int id);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task<T?> Delete(int id);
}

public abstract class EfCoreRepository<TEntity, TContext> : IGenericCrudRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    private readonly TContext _context;

    protected EfCoreRepository(TContext context)
    {
        _context = context;
    }

    public async Task<TEntity> Add(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> Delete(int id)
    {
        TEntity? entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            return entity;
        }

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity?> Get(int id) => await _context.Set<TEntity>().FindAsync(id);

    public async Task<List<TEntity>> GetAll() => await _context.Set<TEntity>().ToListAsync();

    public async Task<TEntity> Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}
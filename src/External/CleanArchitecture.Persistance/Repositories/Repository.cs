using CleanArchitecture.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Persistance.Repositories;
internal class Repository<T, Context> : IRepository<T>
    where T : class
    where Context : DbContext
{
    private readonly Context _context;
    private DbSet<T> _entity;

    public Repository(Context context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }

    public void Add(T entity)
    {
        _entity.Add(entity);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _entity.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        await _entity.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
    }

    public bool Any(Expression<Func<T, bool>> predicate)
    {
        return _entity.Any(predicate);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _entity.AnyAsync(predicate, cancellationToken);
    }

    public void Delete(T entity)
    {
        _entity.Remove(entity);
    }

    public async Task DeleteByExpressionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entity = await _entity.Where(predicate).AsNoTracking().FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        _entity.Remove(entity!);
    }

    public async Task DeleteByIdAsync(string id)
    {
        var entity = await _entity.FindAsync(id).ConfigureAwait(false);
        _entity.Remove(entity!);
    }

    public void DeleteRange(ICollection<T> entities)
    {
        _entity.RemoveRange(entities);
    }

    public IQueryable<T> GetAll()
    {
        return _entity.AsNoTracking().AsQueryable();
    }

    public IQueryable<T> GetAllWithTacking()
    {
        return _entity.AsQueryable();
    }

    public T GetByExpression(Expression<Func<T, bool>> predicate)
    {
        var entity = _entity.Where(predicate).AsNoTracking().FirstOrDefault();
        return entity!;
    }

    public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entity = await _entity.Where(predicate).AsNoTracking().FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        return entity!;
    }

    public T GetByExpressionWithTracking(Expression<Func<T, bool>> predicate)
    {
        var entity = _entity.Where(predicate).FirstOrDefault();
        return entity!;
    }

    public async Task<T> GetByExpressionWithTrackingAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entity = await _entity.Where(predicate).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        return entity!;
    }

    public T GetFirst()
    {
        var entity = _entity.AsNoTracking().FirstOrDefault();
        return entity!;
    }

    public async Task<T> GetFirstAsync(CancellationToken cancellationToken = default)
    {
        var entity = await _entity.AsNoTracking().FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        return entity!;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
    {
        return _entity.AsNoTracking().Where(predicate).AsQueryable();
    }

    public IQueryable<T> GetWhereWithTracking(Expression<Func<T, bool>> predicate)
    {
        return _entity.Where(predicate).AsQueryable();
    }

    public void Update(T entity)
    {
        _entity.Update(entity);
    }

    public void UpdateRange(ICollection<T> entities)
    {
        _entity.UpdateRange(entities);
    }
}

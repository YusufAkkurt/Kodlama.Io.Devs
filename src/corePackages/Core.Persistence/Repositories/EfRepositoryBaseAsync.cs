using Core.Persistence.Dynamics;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public class EfRepositoryBaseAsync<TEntity, TContext> : IRepositoryAsync<TEntity> where TEntity : BaseEntity where TContext : DbContext
{
    protected readonly TContext _context;

    public EfRepositoryBaseAsync(TContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> Query() => _context.Set<TEntity>();

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate) => await this.Query().FirstOrDefaultAsync(predicate);

    public async Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, CancellationToken cancellationToken = default, int index = 0, int size = 10, bool enableTracking = true)
    {
        var query = this.Query();

        if (!enableTracking) query = query.AsNoTracking();
        if (include != null) query = include(query);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) query = orderBy(query);

        return await query.ToPaginateAsync(index, size, cancellationToken: cancellationToken);
    }

    public async Task<IPaginate<TEntity>> GetListByDynamicAsync(Dynamic dynamic, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include, CancellationToken cancellationToken = default, int inedx = 0, int size = 10, bool enableTracking = true)
    {
        var query = this.Query().ToDynamic(dynamic);

        if (!enableTracking) query.AsNoTracking();
        if (include != null) query = include(query);

        return await query.ToPaginateAsync(inedx, size, cancellationToken: cancellationToken);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();

        return entity;
    }
}
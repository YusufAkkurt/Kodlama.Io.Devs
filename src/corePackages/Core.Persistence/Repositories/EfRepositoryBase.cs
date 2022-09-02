using Core.Persistence.Dynamics;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity> where TEntity : BaseEntity where TContext : DbContext
{
    protected readonly TContext _context;

    public EfRepositoryBase(TContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> Query() => _context.Set<TEntity>();

    public TEntity Get(Expression<Func<TEntity, bool>> predicate) => this.Query().FirstOrDefault(predicate);

    public IPaginate<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include, int index = 0, int size = 10, bool enableTracking = true)
    {
        var query = this.Query();

        if (!enableTracking) query = query.AsNoTracking();
        if (include != null) query = include(query);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null) query = orderBy(query);

        return query.ToPaginate(index, size);
    }

    public IPaginate<TEntity> GetListByDynamic(Dynamic dynamic, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include, int index = 0, int size = 10, bool enableTracking = true)
    {
        var query = this.Query().ToDynamic(dynamic);

        if (!enableTracking) query = query.AsNoTracking();
        if (include != null) query = include(query);

        return query.ToPaginate(index, size);
    }

    public TEntity Add(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        _context.SaveChanges();

        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();

        return entity;
    }

    public TEntity Delete(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        _context.SaveChanges();

        return entity;
    }
}
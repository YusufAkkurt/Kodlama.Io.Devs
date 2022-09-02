using Core.Persistence.Dynamics;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public interface IRepositoryAsync<T> : IQuery<T> where T : BaseEntity
{
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);

    Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include,
            CancellationToken cancellationToken = default,
            int index = 0, int size = 10, bool enableTracking = true);

    Task<IPaginate<T>> GetListByDynamicAsync(Dynamic dynamic, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include,
        CancellationToken cancellationToken = default, int inedx = 0, int size = 10, bool enableTracking = true);

    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}
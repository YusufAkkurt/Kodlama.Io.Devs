using Core.Persistence.Dynamics;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public interface IRepository<T> : IQuery<T> where T : BaseEntity
{
    T Get(Expression<Func<T, bool>> predicate);

    IPaginate<T> GetList(Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include,
        int index = 0, int size = 10, bool enableTracking = true);

    IPaginate<T> GetListByDynamic(Dynamic dynamic, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include, int index = 0, int size = 10, bool enableTracking = true);

    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);
}
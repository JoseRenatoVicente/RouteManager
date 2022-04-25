using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RouteManager.Domain.Repository
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> UpdateAllAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);
        Task<bool> RemoveAsync(TEntity entityRemove);
        Task<bool> RemoveAsync(string id);
        Task<long> Count();
    }
}

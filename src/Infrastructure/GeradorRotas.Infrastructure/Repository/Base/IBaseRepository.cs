using GeradorRotas.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GeradorRotas.Infrastructure.Repository.Base
{
    public interface IBaseRepository<TEntity> : IDisposable
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> where);
        Task DeleteAsync(string id);
        Task DeleteRangeAsync(Func<TEntity, bool> where);
        Task<bool> Existe(Func<TEntity, bool> where);
        Task<Tuple<IEnumerable<TEntity>, int>> GetAll(int skip, int take, bool asNoTracking = true);
        Task<IQueryable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByIdAsync(string id);
        Task<IQueryable<TEntity>> ListarPor(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> ObterPor(Expression<Func<TEntity, bool>> where);
        Task SaveChangesAsync();
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
    }
}
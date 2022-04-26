using MongoDB.Driver;
using RouteManager.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RouteManager.Domain.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
    {
        public IMongoCollection<TEntity> DbSet;

        public BaseRepository(IMongoDatabase database)
        {
            DbSet = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await DbSet.Find(entity => true).ToListAsync();

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> where) =>
            await DbSet.Find(where).ToListAsync();

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where) =>
            await DbSet.Find(where).FirstOrDefaultAsync();

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.InsertOneAsync(entity);
            return entity;
        }
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await DbSet.ReplaceOneAsync(c => c.Id == entity.Id, entity);
            return entity;
        }

        public virtual async Task<bool> UpdateAllAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            return (await DbSet.UpdateManyAsync(filter, update)).IsAcknowledged;
        }

        public virtual async Task<bool> RemoveAsync(TEntity entityRemove) =>
            (await DbSet.DeleteOneAsync(entity => entity.Id == entityRemove.Id)).DeletedCount != 0;

        public virtual async Task<bool> RemoveAsync(string id) =>
           (await DbSet.DeleteOneAsync(entity => entity.Id == id)).DeletedCount != 0;

        public virtual async Task<long> Count() =>
            await DbSet.CountDocumentsAsync(entity => true);
    }
}

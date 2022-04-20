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

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await DbSet.Find(entity => true).ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> where) =>
            await DbSet.Find(where).ToListAsync();

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> where) =>
            await DbSet.Find(where).FirstOrDefaultAsync();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.InsertOneAsync(entity);
            return entity;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await DbSet.ReplaceOneAsync(c => c.Id == entity.Id, entity);
            return entity;
        }

        public async Task RemoveAsync(TEntity entityRemove) =>
            await DbSet.DeleteOneAsync(entity => entity.Id == entityRemove.Id);

        public async Task RemoveAsync(string id) =>
            await DbSet.DeleteOneAsync(entity => entity.Id == id);
    }
}

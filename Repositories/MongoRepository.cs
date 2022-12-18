using System.Linq.Expressions;
using MongoDB.Driver;
using ScreenHandler.Models;
using ScreenHandler.Repositories;

namespace ScreenHandler.MongoDB;

public class MongoRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
    where TEntity : IEntity<TIdentifier>
    where TIdentifier : struct
{
    private readonly IMongoCollection<TEntity> _dbCollection;
    private readonly FilterDefinitionBuilder<TEntity> _filterBuilder = Builders<TEntity>.Filter;

    public MongoRepository(IMongoDatabase database, string collectionName) => _dbCollection = database.GetCollection<TEntity>(collectionName);

    public async Task CreateAsync(TEntity entity) => await _dbCollection.InsertOneAsync(entity);

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> filter) => await _dbCollection.DeleteOneAsync(filter);

    public async Task DeleteAsync(TIdentifier id)
    {
        var filter = _filterBuilder.Eq(e => e.Id, id);
        await _dbCollection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _dbCollection.Find(_filterBuilder.Empty)
            .ToListAsync();

        if (!entities.Any())
            return Enumerable.Empty<TEntity>(); ;

        return entities;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
    {
        var entities = await _dbCollection.Find(filter)
            .ToListAsync();

        if (!entities.Any())
            return Enumerable.Empty<TEntity>(); ;

        return entities;
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        var entity = await _dbCollection.Find(filter)
            .FirstOrDefaultAsync();

        if (entity is null)
            return default;

        return entity;
    }

    public async Task<TEntity?> GetAsync(TIdentifier id)
    {
        var filter = _filterBuilder.Eq(e => e.Id, id);
        var entity = await _dbCollection.Find(filter)
            .FirstOrDefaultAsync();

        if (entity is null)
            return default;

        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var filter = _filterBuilder.Eq(e => e.Id, entity.Id);
        await _dbCollection.ReplaceOneAsync(filter, entity);
    }
}

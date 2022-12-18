using System.Linq.Expressions;
using ScreenHandler.Entities;

namespace ScreenHandler.Repositories;

public interface IRepository<TEntity, TIdentifier>
    where TEntity : IEntity<TIdentifier>
    where TIdentifier : struct
{
    Task CreateAsync(TEntity entity);
    Task DeleteAsync(Expression<Func<TEntity, bool>> filter);
    Task DeleteAsync(TIdentifier id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity?> GetAsync(TIdentifier id);
    Task UpdateAsync(TEntity entity);
}

using System.Linq.Expressions;
using ScreenHandler.Entities;

namespace ScreenHandler.Repositories;

public interface IRepository<TEntity>
    where TEntity : IEntity
{
    Task CreateAsync(TEntity entity);
    Task DeleteAsync(Expression<Func<TEntity, bool>> filter);
    Task DeleteAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity?> GetAsync(int id);
    Task UpdateAsync(TEntity entity);
}

using DAL.Models.Base;
using System.Linq.Expressions;

namespace DAL.Repository;

public interface IRepositoryManager
{
    Task<IEnumerable<T>> GetRangeAsync<T>(Expression<Func<T, bool>> queryOptions) where T : class, IBaseEntity;
    Task<T> GetEntityByIdAsync<T>(int id) where T : class, IBaseEntity;
    Task<int> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity;
    Task<bool> UpdateEntityAsync<T>(T entity, Expression<Func<T, object>>[] propertySelectors) where T : class, IBaseEntity;
    Task<int> InsertEntityAsync<T>(T entity) where T : class, IBaseEntity;
    Task<int> InsertRangeAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity;
    Task<bool> DeleteEntityAsync<T>(int id) where T : class, IBaseEntity, new();
    Task<int> DeleteRangeAsync<T>(IEnumerable<int> ids) where T : class, IBaseEntity, new();
}

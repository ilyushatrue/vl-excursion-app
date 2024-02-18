using DAL.Models.Base;
using System.Linq.Expressions;

namespace DAL.Repository;

public interface IRepositoryManager
{
    IEnumerable<T> GetAllEntities<T>(Expression<Func<T, bool>> queryOptions) where T : class, IBaseEntity;
    T GetEntityById<T>(int id) where T : class, IBaseEntity;
    int UpdateRange<T>(IEnumerable<T> entities) where T : class, IBaseEntity;
    bool UpdateEntity<T>(T entity, Expression<Func<T, object>>[] propertySelectors) where T : class, IBaseEntity;
    int InsertEntity<T>(T entity) where T : class, IBaseEntity;
    int InsertRange<T>(IEnumerable<T> entities) where T : class, IBaseEntity;
    bool DeleteEntity<T>(int id) where T : class, IBaseEntity, new();
    int DeleteRange<T>(IEnumerable<int> ids) where T : class, IBaseEntity, new();
}

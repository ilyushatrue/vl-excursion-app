using Microsoft.EntityFrameworkCore;
using DAL.Models.Base;
using System.Linq.Expressions;

namespace DAL.Repository;

public class RepositoryManager(Context context) : IRepositoryManager
{

    public int InsertEntity<T>(T entity) where T : class, IBaseEntity
    {
        context.Entry(entity).State = EntityState.Added;
        context.SaveChanges();
        return entity.Id;
    }

    public int InsertRange<T>(IEnumerable<T> entities) where T : class, IBaseEntity
    {
        foreach (var entity in entities)
            context.Entry(entity).State = EntityState.Added;
        return context.SaveChanges();
    }

    public bool DeleteEntity<T>(int id) where T : class, IBaseEntity, new()
    {
        var dbSet = context.Set<T>();
        var entity = new T { Id = id };
        context.Attach(entity);
        dbSet.Remove(entity);
        context.SaveChanges();
        return true;
    }

    public int DeleteRange<T>(IEnumerable<int> ids) where T : class, IBaseEntity, new()
    {
        var dbSet = context.Set<T>();
        foreach (var id in ids)
        {
            var entity = new T { Id = id };
            context.Attach(entity);
            dbSet.Remove(entity);
            context.SaveChanges();
        }
        return ids.Count();
    }

    public IEnumerable<T> GetAllEntities<T>(Expression<Func<T, bool>> queryOptions) where T : class, IBaseEntity
    {
        var dbSet = context.Set<T>();
        var query = dbSet.Where(queryOptions);
        return query.ToList();
    }

    public T GetEntityById<T>(int id) where T : class, IBaseEntity
    {
        var dbSet = context.Set<T>();
        var query = dbSet.Where(x => x.Id == id).FirstOrDefault() ?? throw new Exception("Error found");
        return query;
    }

    public bool UpdateEntity<T>(T entity, Expression<Func<T, object>>[] propertySelectors) where T : class, IBaseEntity
    {
        context.Attach(entity);

        if (propertySelectors.Length > 0)
        {
            foreach (var selector in propertySelectors)
                context.Entry(entity).Property(selector).IsModified = true;
        }
        else
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        context.SaveChanges();
        return true;
    }

    public int UpdateRange<T>(IEnumerable<T> entities) where T : class, IBaseEntity
    {
        context.AttachRange(entities);
        context.Entry(entities).State = EntityState.Modified;
        var affectedRowsNum = context.SaveChanges();
        return affectedRowsNum;
    }
}

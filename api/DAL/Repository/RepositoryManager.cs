using Microsoft.EntityFrameworkCore;
using DAL.Models.Base;
using System.Linq.Expressions;

namespace DAL.Repository;

public class RepositoryManager(Context context) : IRepositoryManager
{

    public async Task<int> InsertEntityAsync<T>(T entity) where T : class, IBaseEntity
    {
        context.Entry(entity).State = EntityState.Added;
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<int> InsertRangeAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity
    {
        foreach (var entity in entities)
            context.Entry(entity).State = EntityState.Added;
        return await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteEntityAsync<T>(int id) where T : class, IBaseEntity, new()
    {
        var dbSet = context.Set<T>();
        var entity = new T { Id = id };
        context.Attach(entity);
        dbSet.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<int> DeleteRangeAsync<T>(IEnumerable<int> ids) where T : class, IBaseEntity, new()
    {
        var dbSet = context.Set<T>();
        foreach (var id in ids)
        {
            var entity = new T { Id = id };
            context.Attach(entity);
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
        return ids.Count();
    }

    public async Task<IEnumerable<T>> GetRangeAsync<T>(Expression<Func<T, bool>>? queryOptions = null) where T : class, IBaseEntity
    {
        var dbSet = context.Set<T>();
        var query = dbSet.AsQueryable();
        query = queryOptions != null ? dbSet.Where(queryOptions) : query;
        return await query.ToListAsync();
    }

    public async Task<T> GetEntityByIdAsync<T>(int id) where T : class, IBaseEntity
    {
        var dbSet = context.Set<T>();
        var query = await dbSet.Where(x => x.Id == id).FirstOrDefaultAsync() ?? throw new Exception("Error found");
        return query;
    }

    public async Task<bool> UpdateEntityAsync<T>(T entity, Expression<Func<T, object>>[]? propertySelectors = null) where T : class, IBaseEntity
    {
        context.Attach(entity);

        if (propertySelectors?.Length > 0)
        {
            foreach (var selector in propertySelectors)
                context.Entry(entity).Property(selector).IsModified = true;
        }
        else
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<int> UpdateRangeAsync<T>(IEnumerable<T> entities) where T : class, IBaseEntity
    {
        context.AttachRange(entities);
        context.Entry(entities).State = EntityState.Modified;
        var affectedRowsNum = await context.SaveChangesAsync();
        return affectedRowsNum;
    }
}

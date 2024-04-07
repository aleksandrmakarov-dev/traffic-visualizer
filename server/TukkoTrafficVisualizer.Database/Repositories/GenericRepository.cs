using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using TukkoTrafficVisualizer.Database.Entities;
using TukkoTrafficVisualizer.Database.Interfaces;

namespace TukkoTrafficVisualizer.Database.Repositories;

public class GenericRepository<T>:IGenericRepository<T> where T : Entity 
{
    protected readonly IMongoCollection<T> Collection;
    protected readonly IClientSessionHandle Session;

    public GenericRepository(IMongoClient client, IClientSessionHandle session)
    {
        Collection = client.GetDatabase("tukko").GetCollection<T>(typeof(T).Name);
        Session = session;
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        await Collection.InsertOneAsync(Session, entity);

        return entity;
    }

    public virtual async Task<T> ReplaceAsync(T entity)
    {
       await Collection.ReplaceOneAsync(e=>e.Id == entity.Id,entity);

       return entity;
    }

    public async Task<bool> UpdateAsync(string id, UpdateDefinition<T> update)
    {
        var updateResult = await Collection.UpdateOneAsync(e => e.Id == id, update);
        return updateResult.IsAcknowledged;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        var result = await Collection.DeleteOneAsync(e => e.Id == entity.Id);
        return result.IsAcknowledged;
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        return await Collection.AsQueryable().FirstOrDefaultAsync(e=>e.Id == id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Collection.AsQueryable().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetPageAsync(int page, int size, Expression<Func<T, bool>>? whereExpression = null)
    {
        IMongoQueryable<T> query = Collection.AsQueryable();

        if (whereExpression != null)
        {
            query = query.Where(whereExpression);
        }

        return await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? whereExpression = null)
    {
        IMongoQueryable<T> query = Collection.AsQueryable();

        if (whereExpression != null)
        {
            return await query.CountAsync(whereExpression);
        }

        return await query.CountAsync();
    }
}
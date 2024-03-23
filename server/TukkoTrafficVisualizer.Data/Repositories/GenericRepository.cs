using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using TukkoTrafficVisualizer.Data.Entities;
using TukkoTrafficVisualizer.Data.Interfaces;

namespace TukkoTrafficVisualizer.Data.Repositories;

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

    public virtual async Task<T> UpdateAsync(T entity)
    {
        FilterDefinition<T> filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
        
        await Collection.ReplaceOneAsync(filter,entity);

        return entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        FilterDefinition<T> filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);

        await Collection.DeleteOneAsync(filter);
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
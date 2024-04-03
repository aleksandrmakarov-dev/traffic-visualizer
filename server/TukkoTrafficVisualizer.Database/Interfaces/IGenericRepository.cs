using System.Linq.Expressions;
using TukkoTrafficVisualizer.Database.Entities;

namespace TukkoTrafficVisualizer.Database.Interfaces
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetPageAsync(int page, int size, Expression<Func<T, bool>>? whereExpression = null);
        Task<int> CountAsync(Expression<Func<T, bool>>? whereExpression = null);
    }
}

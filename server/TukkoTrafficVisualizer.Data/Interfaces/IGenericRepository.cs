using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TukkoTrafficVisualizer.Data.Entities;

namespace TukkoTrafficVisualizer.Data.Interfaces
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

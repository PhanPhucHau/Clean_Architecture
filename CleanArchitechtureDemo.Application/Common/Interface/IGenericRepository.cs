using System.Linq.Expressions;

namespace Clean_Architecture.Applicaiton.Common.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task UpdateByIdAsync(int id, T updatedEntity);
    }
}

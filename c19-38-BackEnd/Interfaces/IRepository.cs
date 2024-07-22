using Microsoft.EntityFrameworkCore;

namespace c19_38_BackEnd.Interfaces
{
    public interface IRepository<T>
    {
        public Task AddAsync(T entity);
        public Task DeleteAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(int id);
        public Task EditAsync(T entity, int id);
        public Task SaveChangesAsync();
    }
}

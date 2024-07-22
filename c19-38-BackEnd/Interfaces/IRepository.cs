using Microsoft.EntityFrameworkCore;

namespace c19_38_BackEnd.Interfaces
{
    public interface IRepository<T>
    {
        public Task AddAsync(T entity);
        public Task DeleteAsync(int id);
        public ICollection<T> GetAll();
        public Task<T?> GetByIdAsync(int id);
        public void Update(T entity);
        public Task SaveChangesAsync();
    }
}

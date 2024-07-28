using c19_38_BackEnd.Datos;
using c19_38_BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace c19_38_BackEnd.Repositorio
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DefaultContext _context;
        public Repository(DefaultContext context)
        {
            _context = context;
        }

        protected DbSet<T> Entities => _context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await Entities.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            T entidadABorrar = await GetByIdAsync(id);
            Entities.Remove(entidadABorrar);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task EditAsync(T entity, int id)
        {
            var existEntity = await _context.Set<T>().FindAsync(id);
            if (existEntity != null)
            {
                _context.Entry(existEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

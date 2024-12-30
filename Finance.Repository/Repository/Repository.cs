using Finance.Repository.Interface;
using Finance.Utilities.Database;
using Microsoft.EntityFrameworkCore;

namespace Finance.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            if (item != null)
            {
                _context.Set<T>().Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}

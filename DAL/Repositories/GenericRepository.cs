using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly OnlineShopDbContext _context;
        public GenericRepository(OnlineShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _context.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) =>
            await _context.Set<T>().FindAsync(id);

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
            var entity = await GetByIdAsync(id);
            if (entity == null) return;
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}

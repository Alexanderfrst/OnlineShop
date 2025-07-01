using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly OnlineShopDbContext _context;

        public GenericRepository(OnlineShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) =>
            await _context.Set<T>().ToListAsync(cancellationToken);

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken) =>
            await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null) return;
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
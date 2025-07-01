using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly OnlineShopDbContext _context;
        public FavoriteRepository(OnlineShopDbContext ctx) => _context = ctx;

        public async Task<IEnumerable<Favorite>> GetByUserAsync(int userId, CancellationToken cancellationToken) =>
            await _context.Set<Favorite>()
                          .Include(f => f.Product)
                          .Where(f => f.UserId == userId)
                          .ToListAsync(cancellationToken);

        public async Task AddAsync(Favorite fav, CancellationToken cancellationToken)
        {
            await _context.Set<Favorite>().AddAsync(fav, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(int userId, int productId, CancellationToken cancellationToken)
        {
            var fav = await _context.Set<Favorite>()
                                    .FindAsync(new object[] { userId, productId }, cancellationToken);
            if (fav != null) { _context.Set<Favorite>().Remove(fav); await _context.SaveChangesAsync(cancellationToken); }
        }
    }
}
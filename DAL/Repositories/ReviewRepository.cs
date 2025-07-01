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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        public ReviewRepository(OnlineShopDbContext ctx) : base(ctx) { }

        public async Task<IEnumerable<Review>> GetByProductAsync(int productId, CancellationToken cancellationToken) =>
            await _context.Set<Review>()
                          .Include(r => r.User)
                          .Where(r => r.ProductId == productId)
                          .ToListAsync(cancellationToken);
    }
}
using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetByUserAsync(int userId, CancellationToken cancellationToken) =>
            await _context.Orders
                          .Where(o => o.UserId == userId)
                          .ToListAsync(cancellationToken);

        public async Task<Order> GetDetailsAsync(int orderId, CancellationToken cancellationToken) =>
            await _context.Orders
                          .Include(o => o.Items)
                          .ThenInclude(oi => oi.Product)
                          .SingleOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }
}
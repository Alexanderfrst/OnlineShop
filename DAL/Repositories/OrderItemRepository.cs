using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<IEnumerable<OrderItem>> GetByOrderAsync(int orderId, CancellationToken cancellationToken) =>
            await _context.OrderItems
                          .Where(oi => oi.OrderId == orderId)
                          .ToListAsync(cancellationToken);
    }
}

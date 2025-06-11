using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetByUserAsync(int userId) =>
            await _context.Orders
                          .Where(o => o.UserId == userId)
                          .Include(o => o.Items)
                          .ToListAsync();
    }
}

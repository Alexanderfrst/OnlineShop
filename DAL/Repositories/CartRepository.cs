using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(OnlineShopDbContext ctx) : base(ctx) { }

        public async Task<Cart> GetByUserAsync(int userId) =>
            await _context.Carts
                          .Include(c => c.Items)
                          .ThenInclude(ci => ci.Product)
                          .SingleOrDefaultAsync(c => c.UserId == userId);

        public async Task AddItemAsync(int userId, int productId, int quantity)
        {
            var cart = await GetByUserAsync(userId) ?? new Cart { UserId = userId };
            cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(int itemId)
        {
            var item = await _context.CartItems.FindAsync(itemId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantityAsync(int itemId, int quantity)
        {
            var item = await _context.CartItems.FindAsync(itemId);
            if (item != null)
            {
                item.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<Cart> GetByUserAsync(int userId, CancellationToken cancellationToken) =>
            await _context.Carts
                          .Include(c => c.Items)
                          .ThenInclude(ci => ci.Product)
                          .SingleOrDefaultAsync(c => c.UserId == userId, cancellationToken);

        public async Task AddItemAsync(int userId, int productId, int quantity, CancellationToken cancellationToken)
        {
            var cart = await GetByUserAsync(userId, cancellationToken)
                       ?? new Cart { UserId = userId };
            cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveItemAsync(int itemId, CancellationToken cancellationToken)
        {
            var item = await _context.CartItems.FindAsync(new object[] { itemId }, cancellationToken);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateQuantityAsync(int itemId, int quantity, CancellationToken cancellationToken)
        {
            var item = await _context.CartItems.FindAsync(new object[] { itemId }, cancellationToken);
            if (item != null)
            {
                item.Quantity = quantity;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
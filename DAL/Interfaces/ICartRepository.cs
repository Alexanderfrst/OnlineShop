using DAL.Models;

namespace DAL.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetByUserAsync(int userId, CancellationToken cancellationToken);
        Task AddItemAsync(int userId, int productId, int quantity, CancellationToken cancellationToken);
        Task RemoveItemAsync(int itemId, CancellationToken cancellationToken);
        Task UpdateQuantityAsync(int itemId, int quantity, CancellationToken cancellationToken);
    }
}

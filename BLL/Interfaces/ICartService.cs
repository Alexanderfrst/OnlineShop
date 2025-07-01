using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetByUserIdAsync(int? userId, CancellationToken cancellationToken);
        Task AddItemAsync(int? userId, int productId, int quantity, CancellationToken cancellationToken);
        Task RemoveItemAsync(int? userId, int itemId, CancellationToken cancellationToken);
        Task UpdateQuantityAsync(int? userId, int itemId, int quantity, CancellationToken cancellationToken);
    }
}

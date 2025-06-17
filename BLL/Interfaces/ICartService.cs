using BLL.DTO;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetByUserIdAsync(int userId);
        Task AddItemAsync(int userId, int productId, int quantity);
        Task RemoveItemAsync(int userId, int itemId);
        Task UpdateQuantityAsync(int userId, int itemId, int quantity);
    }
}
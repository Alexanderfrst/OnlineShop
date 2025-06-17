using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetByUserAsync(int userId);
        Task AddItemAsync(int userId, int productId, int quantity);
        Task RemoveItemAsync(int itemId);
        Task UpdateQuantityAsync(int itemId, int quantity);
    }
}

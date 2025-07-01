using DAL.Models;

namespace DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetByUserAsync(int userId, CancellationToken cancellationToken);
        Task<Order> GetDetailsAsync(int orderId, CancellationToken cancellationToken);
    }
}

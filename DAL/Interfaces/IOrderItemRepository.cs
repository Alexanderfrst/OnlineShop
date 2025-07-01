using DAL.Models;

namespace DAL.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetByOrderAsync(int orderId, CancellationToken cancellationToken);
    }
}

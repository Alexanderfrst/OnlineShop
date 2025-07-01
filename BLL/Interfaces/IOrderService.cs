using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetByUserAsync(int userId, CancellationToken cancellationToken);

        Task<OrderDto> CreateOrderAsync(
            int userId,
            string deliveryMethod,
            string promoCode,
            CancellationToken cancellationToken);

        Task<OrderDto> UpdateStatusAsync(
            int orderId,
            string newStatus,
            CancellationToken cancellationToken);
    }
}

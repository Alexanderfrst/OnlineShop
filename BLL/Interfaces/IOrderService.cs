using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetByUserAsync(int userId);
        Task<OrderDto> CreateOrderAsync(int userId);
    }
}
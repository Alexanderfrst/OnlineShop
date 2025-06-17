using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepo,
            ICartRepository cartRepo,
            IMapper mapper)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(int userId)
        {
            // Получаем корзину с продуктами
            var cart = await _cartRepo.GetByUserAsync(userId);
            if (cart == null || !cart.Items.Any())
                throw new InvalidOperationException("Корзина пуста");

            // Формируем заказ
            var order = new Order
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Status = "Created",
                TotalPrice = cart.Items.Sum(i => i.Quantity * i.Product.Price),
                Items = cart.Items.Select(ci => new OrderItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    UnitPrice = ci.Product.Price
                }).ToList()
            };

            await _orderRepo.AddAsync(order);

            // Опционально: очистить корзину
            // await _cartRepo.ClearAsync(userId);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetByUserAsync(int userId)
        {
            var orders = await _orderRepo.GetByUserAsync(userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}

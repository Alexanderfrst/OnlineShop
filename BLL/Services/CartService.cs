using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepo, IMapper mapper)
        {
            _cartRepo = cartRepo;
            _mapper = mapper;
        }

        public async Task<CartDto> GetByUserIdAsync(int? userId, CancellationToken cancellationToken)
        {
            var uid = userId.GetValueOrDefault();
            var cart = await _cartRepo.GetByUserAsync(uid, cancellationToken);

            if (cart == null)
                return new CartDto(0, null, new List<CartItemDto>(), 0m);

            var userDto = _mapper.Map<UserDto>(cart.User);
            var itemsDto = _mapper.Map<List<CartItemDto>>(cart.Items);

            var totalPrice = itemsDto.Sum(i => i.Product.Price * i.Quantity);

            return new CartDto(
                cart.Id,
                userDto,
                itemsDto,
                totalPrice
            );
        }

        public async Task AddItemAsync(int? userId, int productId, int quantity, CancellationToken cancellationToken)
        {
            var uid = userId.GetValueOrDefault();
            await _cartRepo.AddItemAsync(uid, productId, quantity, cancellationToken);
        }

        public async Task RemoveItemAsync(int? userId, int itemId, CancellationToken cancellationToken)
        {
            await _cartRepo.RemoveItemAsync(itemId, cancellationToken);
        }

        public async Task UpdateQuantityAsync(int? userId, int itemId, int quantity, CancellationToken cancellationToken)
        {
            await _cartRepo.UpdateQuantityAsync(itemId, quantity, cancellationToken);
        }
    }
}

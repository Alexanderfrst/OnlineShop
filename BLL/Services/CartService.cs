using System.Threading.Tasks;
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

        public async Task<CartDto> GetByUserIdAsync(int userId) =>
            _mapper.Map<CartDto>(await _cartRepo.GetByUserAsync(userId));

        public async Task AddItemAsync(int userId, int productId, int quantity) =>
            await _cartRepo.AddItemAsync(userId, productId, quantity);

        public async Task RemoveItemAsync(int userId, int itemId) =>
            await _cartRepo.RemoveItemAsync(itemId);

        public async Task UpdateQuantityAsync(int userId, int itemId, int quantity) =>
            await _cartRepo.UpdateQuantityAsync(itemId, quantity);
    }
}
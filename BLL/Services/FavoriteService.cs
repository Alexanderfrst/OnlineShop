using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repo;
        private readonly IMapper _mapper;
        public FavoriteService(IFavoriteRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

        public async Task<IEnumerable<ProductDto>> GetFavoritesAsync(int userId, CancellationToken cancellationToken)
        {
            var favs = await _repo.GetByUserAsync((int)userId, cancellationToken);
            var products = new List<DAL.Models.Product>();
            foreach (var f in favs) products.Add(f.Product);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task AddAsync(int userId, int productId, CancellationToken cancellationToken)
        {
            var fav = new Favorite { UserId = (int)userId, ProductId = (int)productId, AddedAt = DateTime.UtcNow };
            await _repo.AddAsync(fav, cancellationToken);
        }

        public async Task RemoveAsync(int userId, int productId, CancellationToken cancellationToken)
        {
            await _repo.RemoveAsync((int)userId, (int)productId, cancellationToken);
        }
    }
}
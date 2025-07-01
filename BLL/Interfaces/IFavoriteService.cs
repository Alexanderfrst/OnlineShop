using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IFavoriteService
    {
        Task<IEnumerable<ProductDto>> GetFavoritesAsync(int userId, CancellationToken cancellationToken);
        Task AddAsync(int userId, int productId, CancellationToken cancellationToken);
        Task RemoveAsync(int userId, int productId, CancellationToken cancellationToken);
    }
}
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<IEnumerable<Favorite>> GetByUserAsync(int userId, CancellationToken cancellationToken);
        Task AddAsync(Favorite fav, CancellationToken cancellationToken);
        Task RemoveAsync(int userId, int productId, CancellationToken cancellationToken);
    }
}
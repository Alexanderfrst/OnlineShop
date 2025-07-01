using DAL.Models;

namespace DAL.Interfaces
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<IEnumerable<Review>> GetByProductAsync(int productId, CancellationToken cancellationToken);
    }
}
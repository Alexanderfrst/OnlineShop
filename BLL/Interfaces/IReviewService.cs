using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetByProductAsync(int productId, CancellationToken cancellationToken);
        Task AddAsync(int productId, ReviewDto review, CancellationToken cancellationToken);
    }
}
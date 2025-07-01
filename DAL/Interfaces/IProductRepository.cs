using DAL.Models;

namespace DAL.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> SearchAsync(string term, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetFilteredAsync(
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            bool? inStock,
            string sortBy,          // "price", "rating", "newness"
            bool ascending,
            CancellationToken cancellationToken);
    }
}

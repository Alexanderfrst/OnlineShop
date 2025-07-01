using BLL.DTO;


namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<ProductDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken);
        Task<IEnumerable<ProductDto>> SearchAsync(string term, CancellationToken cancellationToken);
        Task CreateAsync(ProductDto product, CancellationToken cancellationToken);
        Task UpdateAsync(ProductDto product, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<ProductDto>> GetFilteredAsync(
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            bool? inStock,
            string sortBy,
            bool ascending,
            CancellationToken cancellationToken);
    }
}

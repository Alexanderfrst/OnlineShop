using BLL.DTO;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<CategoryDto>> GetChildrenAsync(int parentId, CancellationToken cancellationToken);
        Task CreateAsync(CategoryDto category, CancellationToken cancellationToken);
        Task UpdateAsync(CategoryDto category, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
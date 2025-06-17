using BLL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetChildrenAsync(int parentId);
        Task CreateAsync(CategoryDto category);
        Task UpdateAsync(CategoryDto category);
        Task DeleteAsync(int id);
    }
}
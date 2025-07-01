using DAL.Models;

namespace DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetChildrenAsync(int parentId, CancellationToken cancellationToken);
    }
}
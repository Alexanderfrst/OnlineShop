using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetChildrenAsync(int parentId,CancellationToken cancellationToken) =>
            await _context.Categories
                          .Where(c => c.ParentCategoryId == parentId)
                          .ToListAsync(cancellationToken);
    }
}

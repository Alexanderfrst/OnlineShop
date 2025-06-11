using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> GetChildrenAsync(int parentId) =>
            await _context.Categories
                          .Where(c => c.ParentCategoryId == parentId)
                          .ToListAsync();
    }
}

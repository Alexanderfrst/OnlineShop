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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId) =>
            await _context.Products
                          .Where(p => p.CategoryId == categoryId)
                          .ToListAsync();

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm) =>
            await _context.Products
                          .Where(p => p.Name.Contains(searchTerm)
                                   || p.Description.Contains(searchTerm))
                          .ToListAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;


namespace DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly OnlineShopDbContext _context;

        public ProductRepository(OnlineShopDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string term, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Where(p => EF.Functions.Like(p.Name, $"%{term}%"))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetFilteredAsync(
            int? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            bool? inStock,
            string sortBy,
            bool ascending,
            CancellationToken cancellationToken)
        {
            var q = _context.Products
                .Include(p => p.Images)
                .AsQueryable();

            if (categoryId.HasValue)
                q = q.Where(p => p.CategoryId == categoryId.Value);

            if (minPrice.HasValue)
                q = q.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                q = q.Where(p => p.Price <= maxPrice.Value);

            if (inStock.HasValue)
                q = inStock.Value
                    ? q.Where(p => p.StockQuantity > 0)
                    : q.Where(p => p.StockQuantity == 0);

            switch (sortBy?.ToLower())
            {
                case "price":
                    q = ascending
                        ? q.OrderBy(p => p.Price)
                        : q.OrderByDescending(p => p.Price);
                    break;

                case "rating":
                    q = ascending
                        ? q.OrderBy(p =>
                            _context.Reviews
                                .Where(r => r.ProductId == p.Id)
                                .Select(r => (double?)r.Rating)
                                .Average() ?? 0)
                        : q.OrderByDescending(p =>
                            _context.Reviews
                                .Where(r => r.ProductId == p.Id)
                                .Select(r => (double?)r.Rating)
                                .Average() ?? 0);
                    break;

                case "newness":
                    q = ascending
                        ? q.OrderBy(p => p.Id)
                        : q.OrderByDescending(p => p.Id);
                    break;

                default:
                    break;
            }

            return await q.ToListAsync(cancellationToken);
        }
    }
}

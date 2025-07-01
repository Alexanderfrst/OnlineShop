using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PromoCodeRepository : GenericRepository<PromoCode>, IPromoCodeRepository
    {
        public PromoCodeRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<PromoCode> GetByCodeAsync(string code, CancellationToken cancellationToken) =>
            await _context.PromoCodes
                          .SingleOrDefaultAsync(pc => pc.Code == code, cancellationToken);

        public async Task<bool> IsValidAsync(string code, CancellationToken cancellationToken)
        {
            var promo = await GetByCodeAsync(code, cancellationToken);
            return promo != null && promo.IsActive && promo.ExpiryDate >= DateTime.UtcNow;
        }
    }
}
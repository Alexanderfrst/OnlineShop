using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PromoCodeRepository : GenericRepository<PromoCode>, IPromoCodeRepository
    {
        public PromoCodeRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<PromoCode> GetByCodeAsync(string code) =>
            await _context.PromoCodes.SingleOrDefaultAsync(pc => pc.Code == code);
    }
}

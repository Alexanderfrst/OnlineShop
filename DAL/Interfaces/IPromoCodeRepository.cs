using DAL.Models;

namespace DAL.Interfaces
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        Task<PromoCode> GetByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> IsValidAsync(string code, CancellationToken cancellationToken);
    }
}

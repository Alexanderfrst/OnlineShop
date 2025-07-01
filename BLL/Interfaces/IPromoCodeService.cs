using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IPromoCodeService
    {
        Task<PromoCodeDto> GetByCodeAsync(string code, CancellationToken cancellationToken);
        Task<bool> IsValidAsync(string v, CancellationToken none);
    }
}

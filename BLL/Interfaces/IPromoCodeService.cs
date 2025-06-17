using BLL.DTO;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPromoCodeService
    {
        Task<PromoCodeDto> GetByCodeAsync(string code);
    }
}
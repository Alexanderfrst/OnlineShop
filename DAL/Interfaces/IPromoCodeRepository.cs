using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        Task<PromoCode> GetByCodeAsync(string code);
    }
}

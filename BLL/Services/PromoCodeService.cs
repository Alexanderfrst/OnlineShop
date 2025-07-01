using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly IMapper _mapper;

        public PromoCodeService(IPromoCodeRepository promoCodeRepository, IMapper mapper)
        {
            _promoCodeRepository = promoCodeRepository;
            _mapper = mapper;
        }

        public async Task<PromoCodeDto> GetByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var promoCode = await _promoCodeRepository.GetByCodeAsync(code, cancellationToken);
            return _mapper.Map<PromoCodeDto>(promoCode);
        }

        public async Task<bool> IsValidAsync(string code, CancellationToken cancellationToken)
        {
            var promoCode = await _promoCodeRepository.GetByCodeAsync(code, cancellationToken);
            return promoCode != null && promoCode.IsActive && promoCode.ExpiryDate >= DateTime.UtcNow;
        }
    }
}

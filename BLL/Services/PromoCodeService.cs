using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IPromoCodeRepository _promoRepo;
        private readonly IMapper _mapper;

        public PromoCodeService(IPromoCodeRepository promoRepo, IMapper mapper)
        {
            _promoRepo = promoRepo;
            _mapper = mapper;
        }

        public async Task<PromoCodeDto> GetByCodeAsync(string code) =>
            _mapper.Map<PromoCodeDto>(await _promoRepo.GetByCodeAsync(code));
    }
}
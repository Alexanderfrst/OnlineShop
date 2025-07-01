using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTO;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodesController : ControllerBase
    {
        private readonly IPromoCodeService _promoCodeService;

        public PromoCodesController(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<PromoCodeDto>> GetByCode(string code, CancellationToken cancellationToken)
        {
            var promoCode = await _promoCodeService.GetByCodeAsync(code, cancellationToken);
            if (promoCode == null)
                return NotFound();
            return Ok(promoCode);
        }
    }
}

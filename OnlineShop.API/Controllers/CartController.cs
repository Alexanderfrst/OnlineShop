using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTO;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<CartDto>> GetByUserId(int userId, CancellationToken cancellationToken)
        {
            var cart = await _cartService.GetByUserIdAsync(userId, cancellationToken);
            return Ok(cart);
        }

        [HttpPost("{userId}/add-item")]
        public async Task<ActionResult> AddItem(int userId, [FromBody] CartItemDto cartItemDto, CancellationToken cancellationToken)
        {
            await _cartService.AddItemAsync(userId, cartItemDto.Product.Id, cartItemDto.Quantity, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{userId}/remove-item/{itemId}")]
        public async Task<ActionResult> RemoveItem(int userId, int itemId, CancellationToken cancellationToken)
        {
            await _cartService.RemoveItemAsync(userId, itemId, cancellationToken);
            return NoContent();
        }

        [HttpPut("{userId}/update-quantity/{itemId}")]
        public async Task<ActionResult> UpdateQuantity(int userId, int itemId, [FromBody] int quantity, CancellationToken cancellationToken)
        {
            await _cartService.UpdateQuantityAsync(userId, itemId, quantity, cancellationToken);
            return NoContent();
        }
    }
}

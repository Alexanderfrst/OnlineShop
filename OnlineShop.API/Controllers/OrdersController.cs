using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTO;
using OnlineShop.API.Models;


namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByUserId(int userId, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetByUserAsync(userId, cancellationToken);
            return Ok(orders);
        }

        [HttpPost("create-order")]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderService.CreateOrderAsync(request.UserId, request.DeliveryMethod, request.PromoCode, cancellationToken);
            return CreatedAtAction(nameof(GetByUserId), new { userId = request.UserId }, order);
        }
    }
}

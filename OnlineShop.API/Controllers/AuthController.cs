using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using BLL.DTO;
using Microsoft.AspNetCore.Identity.Data;


namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password, cancellationToken);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            await _authService.RegisterAsync(request.Email, request.Password, cancellationToken);
            return NoContent();
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            await _authService.ResetPasswordAsync(request.Email, cancellationToken);
            return NoContent();
        }
    }
}

using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(string email, string password, CancellationToken cancellationToken);
        Task<string> LoginAsync(string email, string password, CancellationToken cancellationToken);
        Task ResetPasswordAsync(string email, CancellationToken cancellationToken);
        Task<UserDto> GetProfileAsync(int userId, CancellationToken cancellationToken);
        Task UpdateProfileAsync(UserDto user, CancellationToken cancellationToken);
    }
}
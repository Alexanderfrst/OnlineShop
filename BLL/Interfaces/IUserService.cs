using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<UserDto> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task CreateAsync(UserDto user, CancellationToken cancellationToken);
        Task UpdateAsync(UserDto user, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
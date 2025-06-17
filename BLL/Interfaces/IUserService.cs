using BLL.DTO;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> GetByEmailAsync(string email);
        Task CreateAsync(UserDto user);
        Task UpdateAsync(UserDto user);
        Task DeleteAsync(int id);
    }
}
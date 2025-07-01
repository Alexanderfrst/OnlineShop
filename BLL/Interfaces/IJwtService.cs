using DAL.Models;

namespace BLL.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}

using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<User> GetByEmailAsync(string email) =>
            await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}

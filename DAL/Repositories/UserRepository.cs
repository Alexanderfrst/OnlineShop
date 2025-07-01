using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(OnlineShopDbContext context) : base(context) { }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
            await _context.Users
                          .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}
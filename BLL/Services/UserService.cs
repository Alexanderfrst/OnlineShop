using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserDto> GetByIdAsync(int id) =>
            _mapper.Map<UserDto>(await _userRepo.GetByIdAsync(id));

        public async Task<UserDto> GetByEmailAsync(string email) =>
            _mapper.Map<UserDto>(await _userRepo.GetByEmailAsync(email));

        public async Task CreateAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepo.AddAsync(user);
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepo.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id) =>
            await _userRepo.DeleteAsync(id);
    }
}
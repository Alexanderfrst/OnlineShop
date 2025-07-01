using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

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

        public async Task<UserDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync((int)id, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByEmailAsync(email, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }

        public async Task CreateAsync(UserDto dto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(dto);
            user.CreatedAt = DateTime.UtcNow;
            await _userRepo.AddAsync(user, cancellationToken);
        }

        public async Task UpdateAsync(UserDto dto, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(dto);
            await _userRepo.UpdateAsync(user, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _userRepo.DeleteAsync((int)id, cancellationToken);
        }
    }
}
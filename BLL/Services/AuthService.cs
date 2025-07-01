using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(
            IUserRepository userRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = configuration;
        }

        public async Task<string> RegisterAsync(string email, string password, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email.");

            var existing = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (existing != null)
                throw new InvalidOperationException("User already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Email = email,
                PasswordHash = hashedPassword,
                Role = "Customer",
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user, cancellationToken);
            return GenerateJwtToken(user);
        }

        public async Task<string> LoginAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return GenerateJwtToken(user);
        }

        public async Task ResetPasswordAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
            if (user == null)
                throw new ArgumentException("User not found.");

            var resetToken = GeneratePasswordResetToken(user);
            var resetLink = $"{_config["Frontend:BaseUrl"]}/reset-password?token={resetToken}";

            await SendPasswordResetEmail(user.Email, resetLink);
        }

        private string GeneratePasswordResetToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("sub", user.Id.ToString()),
                new Claim("reset", "true")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task SendPasswordResetEmail(string userEmail, string resetLink)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config["Email:FromName"], _config["Email:FromAddress"]));
            message.To.Add(new MailboxAddress(userEmail, userEmail));
            message.Subject = "Password Reset Request";

            var body = $"<p>Click <a href=\"{resetLink}\">here</a> to reset your password.</p>";
            message.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_config["Email:SmtpHost"], int.Parse(_config["Email:SmtpPort"]), false);
                await client.AuthenticateAsync(_config["Email:SmtpUser"], _config["Email:SmtpPass"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task<UserDto> GetProfileAsync(int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID.");

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }

        public async Task UpdateProfileAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            if (userDto == null || userDto.Id <= 0)
                throw new ArgumentException("Invalid user data.");

            var user = _mapper.Map<User>(userDto);
            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("sub", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string password, string storedHash)
            => BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
}
using Azure.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TasktifyAPI.Models;
using TasktifyAPI.Models.Dtos;
using TasktifyAPI.Repositories.Contracts;
using TasktifyAPI.Services.Contracts;
using TasktifyAPI.Services.Helpers;
using Task = TasktifyAPI.Models.Task;

namespace TasktifyAPI.Services.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepositry;
        private readonly PasswordManager _passwordManager;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, PasswordManager passwordManager, IConfiguration configuration)
        {
            _userRepositry = userRepository;
            _passwordManager = passwordManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="userCreateLoginDto"></param>
        /// <returns></returns>
        public async Task<UserDto> RegisterUserAsync(UserCreateLoginDto userCreateLoginDto)
        {
            var user = new User
            {
                Username = userCreateLoginDto.Username,
                PasswordHash = _passwordManager.HashPassword(userCreateLoginDto.Password)
            };

            var createdUser = await _userRepositry.CreateUserAsync(user);

            return new UserDto
            {
                UserId = createdUser.UserId,
                Username = createdUser.Username,
                Tasks = new List<TaskDto>() // no task initially
            };
        }


        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="userCreateLoginDto"></param>
        /// <returns></returns>
        public async Task<string?> LoginAsync(UserCreateLoginDto userCreateLoginDto)
        {
            var user = await _userRepositry.GetUserByUsernameAsync(userCreateLoginDto.Username);

            if(user == null || !_passwordManager.VerifyPassword(user.PasswordHash,userCreateLoginDto.Password))
            {
                return null; // Invalid credentials
            }

            return GenerateJwtToken(user);
        }

        /// <summary>
        /// Generate jwt token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(6),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Get current authenticated user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal userPrincipal)
        {
            var userIdClaim = userPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return null;

            var user = await _userRepositry.GetUserByIdAsync(int.Parse(userIdClaim.Value));
            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Tasks = user.Tasks.Select(task => new TaskDto
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    Description = task.Description,
                    UserId = task.UserId
                }).ToList()
            };
        }
    }
}

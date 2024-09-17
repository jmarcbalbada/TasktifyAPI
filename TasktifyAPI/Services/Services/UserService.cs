using TasktifyAPI.Models;
using TasktifyAPI.Models.Dtos;
using TasktifyAPI.Repositories.Contracts;
using TasktifyAPI.Services.Contracts;
using TasktifyAPI.Services.Helpers;

namespace TasktifyAPI.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordManager _passwordManager;

        public UserService(IUserRepository userRepository, PasswordManager passwordManager)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userCreateDto"></param>
        /// <returns></returns>
        public async Task<UserDto> CreateUserAsync(UserCreateLoginDto userCreateDto)
        {
            var user = new User
            {
                Username = userCreateDto.Username,
                PasswordHash = _passwordManager.HashPassword(userCreateDto.Password)
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            return new UserDto
            {
                UserId = createdUser.UserId,
                Username = createdUser.Username,
                Tasks = new List<TaskDto>()
            };
        }

        /// <summary>
        /// Get User By ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Tasks = user.Tasks.Select(task => new TaskDto()
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    Description = task.Description
                }).ToList()
            };
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(user => new UserDto()
            {
                UserId = user.UserId,
                Username = user.Username,
                Tasks = user.Tasks.Select(task => new TaskDto()
                {
                    TaskId = task.TaskId,
                    TaskName = task.TaskName,
                    Description = task.Description
                }).ToList()
            });
        }
        
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }
    }
}

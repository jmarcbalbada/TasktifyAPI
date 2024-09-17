using TasktifyAPI.Models.Dtos;

namespace TasktifyAPI.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserCreateLoginDto userCreateDto);
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int userId);
    }
}

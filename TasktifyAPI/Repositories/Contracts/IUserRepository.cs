using TasktifyAPI.Models;
using Task = TasktifyAPI.Models.Task;

namespace TasktifyAPI.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int userId);
    }
}

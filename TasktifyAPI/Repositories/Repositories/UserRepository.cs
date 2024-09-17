using Microsoft.EntityFrameworkCore;
using TasktifyAPI.Models;
using TasktifyAPI.Repositories.Context;
using TasktifyAPI.Repositories.Contracts;

namespace TasktifyAPI.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TasktifyContext _context;

        public UserRepository(TasktifyContext context)
        {
            // DI
            _context = context;
        }

        // Create new user
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Get user by ID
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);

        }

        // Get user by Username (auth)
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        
        // Get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync(); 
        }

        // Delete user
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            // early return
            if (user == null) return false;

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

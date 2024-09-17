using Microsoft.AspNetCore.Identity;
using TasktifyAPI.Models;


namespace TasktifyAPI.Services.Helpers
{
    public class PasswordManager
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordManager(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        // Hash password
        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        // Verify plain password to hashed password
        public bool VerifyPassword(string hashedPassword, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}

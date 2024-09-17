using System.Security.Claims;
using TasktifyAPI.Models.Dtos;

namespace TasktifyAPI.Services.Contracts
{
    public interface IAuthService
    {
        Task<UserDto> RegisterUserAsync(UserCreateLoginDto userCreateLoginDto);
        Task<string?> LoginAsync(UserCreateLoginDto userCreateLoginDto);

        // Get the current authenticated user
        Task<UserDto?> GetCurrentUserAsync(ClaimsPrincipal userPrincipal);
    }
}

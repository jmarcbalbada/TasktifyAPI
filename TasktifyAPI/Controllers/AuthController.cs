using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasktifyAPI.Models.Dtos;
using TasktifyAPI.Services.Contracts;

namespace TasktifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="userCreateLoginDto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserCreateLoginDto userCreateLoginDto)
        {
            var result = await _authService.RegisterUserAsync(userCreateLoginDto);
            return Ok(result);  // Return 200 OK with the registered user information
        }

        /// <summary>
        /// Login a user
        /// </summary>
        /// <param name="userCreateLoginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserCreateLoginDto userCreateLoginDto)
        {
            var token = await _authService.LoginAsync(userCreateLoginDto);
            if (token == null)
                return Unauthorized();  // Return 401 Unauthorized if login fails

            return Ok(new { Token = token });  // Return the JWT token if login is successful
        }

        /// <summary>
        /// Get the current authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _authService.GetCurrentUserAsync(User);  // Use the ClaimsPrincipal to get the current user
            if (user == null)
                return NotFound();  // Return 404 if the user is not found

            return Ok(user);
        }
    }
}

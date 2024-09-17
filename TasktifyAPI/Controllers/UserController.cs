using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasktifyAPI.Models.Dtos;
using TasktifyAPI.Services.Contracts;

namespace TasktifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Get user via ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Create user 
        /// cases: admin panel where auth and token is not required
        /// </summary>
        /// <param name="userCreateLoginDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateLoginDto userCreateLoginDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var createdUser = await _userService.CreateUserAsync(userCreateLoginDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        /// <summary>
        /// Delete user via userID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

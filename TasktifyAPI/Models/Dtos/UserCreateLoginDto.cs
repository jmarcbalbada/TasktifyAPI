using System.ComponentModel.DataAnnotations;

namespace TasktifyAPI.Models.Dtos
{
    public class UserCreateLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

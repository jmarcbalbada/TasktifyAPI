using System.ComponentModel.DataAnnotations;

namespace TasktifyAPI.Models.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }
        public IEnumerable<TaskDto> Tasks { get; set; } = new List<TaskDto>(); // Default to an empty list
    }
}

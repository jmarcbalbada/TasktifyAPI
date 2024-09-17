using System.ComponentModel.DataAnnotations;

namespace TasktifyAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]   
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Navigation Property to Tasks
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}

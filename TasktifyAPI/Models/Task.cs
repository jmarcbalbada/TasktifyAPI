using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasktifyAPI.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [MaxLength(50)]
        public string TaskName { get; set; }

        [Required]
        public string Description { get; set; }

        // Foreign Key to User
        [Required]
        public int UserId { get; set; }

        // Navigation Property to User
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

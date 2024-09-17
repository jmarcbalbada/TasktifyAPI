using System.ComponentModel.DataAnnotations;

namespace TasktifyAPI.Models.Dtos
{
    public class TaskDto
    {
        public int TaskId { get; set; }

        [MaxLength(50)]  
        public string TaskName { get; set; }

        [Required]
        public string Description { get; set; }

        // Foreign
        public int UserId { get; set; }
    }
}

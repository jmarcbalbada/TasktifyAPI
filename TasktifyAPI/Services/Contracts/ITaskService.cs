using TasktifyAPI.Models.Dtos;

namespace TasktifyAPI.Services.Contracts
{
    public interface ITaskService
    {
        Task<TaskDto> CreateTaskAsync(TaskDto taskdto, int userId);
        Task<TaskDto?> GetTaskByIdAsync(int taskId);
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<bool> UpdateTaskAsync(TaskDto taskdto, int taskId);
        Task<bool> DeleteTaskAsync(int taskId);

        // Get all tasks by userid
        Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int userId);
    }
}

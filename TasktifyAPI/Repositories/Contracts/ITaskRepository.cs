using TasktifyAPI.Models;
using Task = TasktifyAPI.Models.Task;

namespace TasktifyAPI.Repositories.Contracts
{
    public interface ITaskRepository
    {
        Task<Task> CreateTaskAsync(Task task);
        Task<Task?> GetTaskByIdAsync(int taskId);
        Task<IEnumerable<Task>> GetAllTasksAsync();
        Task<IEnumerable<Task>> GetTasksByUserIdAsync(int userId);
        Task<bool> UpdateTaskAsync(Task task, int taskId);
        Task<bool> DeleteTaskAsync(int taskId);
    }
}

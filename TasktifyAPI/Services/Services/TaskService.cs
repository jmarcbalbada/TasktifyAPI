using TasktifyAPI.Models.Dtos;
using TasktifyAPI.Repositories.Contracts;
using TasktifyAPI.Services.Contracts;
using Task = TasktifyAPI.Models.Task;

namespace TasktifyAPI.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        ///  Create task
        /// </summary>
        /// <param name="taskdto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<TaskDto> CreateTaskAsync(TaskDto taskDto, int userId)
        {
            var task = new Task
            {
                TaskName = taskDto.TaskName,
                Description = taskDto.Description,
                UserId = userId
            };

            var createdTask = await _taskRepository.CreateTaskAsync(task);

            return new TaskDto()
            {
                TaskId = createdTask.TaskId,
                TaskName = createdTask.TaskName,
                Description = createdTask.Description,
                UserId = createdTask.UserId,
            };
        }

        /// <summary>
        /// Get a task by its ID
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<TaskDto?> GetTaskByIdAsync(int taskId)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null) return null;

            return new TaskDto
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                Description = task.Description,
                UserId = task.UserId,
            };
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return tasks.Select(task => new TaskDto
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                Description = task.Description,
                UserId = task.UserId,
            });
        }

        /// <summary>
        /// Update a task
        /// </summary>
        /// <param name="taskdto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTaskAsync(TaskDto taskdto, int taskId)
        {
            if(taskdto == null) return false;

            var task = new Task
            {
                TaskId = taskdto.TaskId,
                TaskName = taskdto.TaskName,
                Description = taskdto.Description,
                UserId = taskdto.UserId,
            };

            return await _taskRepository.UpdateTaskAsync(task, taskId);
        }

        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            return await _taskRepository.DeleteTaskAsync(taskId);
        }

        /// <summary>
        ///  Get tasks by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = await _taskRepository.GetTasksByUserIdAsync(userId);
            return tasks.Select(task => new TaskDto
            {
                TaskId = task.TaskId,
                TaskName = task.TaskName,
                Description = task.Description,
                UserId = task.UserId,
            });
        }
    }
}

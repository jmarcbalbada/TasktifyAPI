using TasktifyAPI.Repositories.Context;
using TasktifyAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Task = TasktifyAPI.Models.Task;

namespace TasktifyAPI.Repositories.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasktifyContext _context;

        public TaskRepository(TasktifyContext context)
        {
            // DI
            _context = context;
        }

        // Create new task
        public async Task<Task> CreateTaskAsync(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        // Get task by ID
        public async Task<Task> GetTaskByIdAsync(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        // Get all tasks
        public async Task<IEnumerable<Task>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        // Get tasks by userID
        public async Task<IEnumerable<Task>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }
        
        // Update task
        public async Task<bool> UpdateTaskAsync(Task task, int taskId)
        {
            var fetchTask = await _context.Tasks.FindAsync(taskId);
            if (fetchTask == null) return false;

            fetchTask.TaskName = task.TaskName;
            fetchTask.Description = task.Description;

            //_context.Tasks.Update(task);
            return await _context.SaveChangesAsync() > 0;
        }
        
        // Delete task
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);

            // early return
            if (task == null) return false;

            _context.Tasks.Remove(task);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

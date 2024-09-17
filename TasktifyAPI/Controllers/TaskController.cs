using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TasktifyAPI.Models.Dtos;
using TasktifyAPI.Services.Contracts;

namespace TasktifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Get task by taskID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        /// <summary>
        /// Get tasks by userID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTasksByUserId(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }

        /// <summary>
        /// create new task along with userID
        /// </summary>
        /// <param name="taskDto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto taskDto, [FromQuery] int userId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var createdTask = await _taskService.CreateTaskAsync(taskDto, userId);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.TaskId }, createdTask);
        }

        /// <summary>
        /// update task query id, body TaskDTO
        /// </summary>
        /// <param name="id"></param>
        /// <param name="taskDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDto taskDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _taskService.UpdateTaskAsync(taskDto, id);
            if (!updated) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Delete task with query id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);

            if(!deleted) return NotFound();

            return NoContent();
        }
    }
}

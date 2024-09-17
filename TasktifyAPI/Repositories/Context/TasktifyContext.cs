using Microsoft.EntityFrameworkCore;
using TasktifyAPI.Models;
using Task = TasktifyAPI.Models.Task;

namespace TasktifyAPI.Repositories.Context
{
    public class TasktifyContext : DbContext
    {
        // tables here
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public TasktifyContext(DbContextOptions <TasktifyContext> options) : base(options){}
    }
}

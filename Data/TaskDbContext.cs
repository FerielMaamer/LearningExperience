using Microsoft.EntityFrameworkCore;

namespace TaskTracker.Data
{
    public class TaskDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=taskmanager.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().HasNoKey();
        }
        
    }
}

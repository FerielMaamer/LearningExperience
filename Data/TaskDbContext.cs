using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;
using TaskTracker.Models;

namespace TaskTracker.Data
{
    public class TaskDbContext : DbContext
    {
        public DbSet<IndividualTask> TasksGroup { get; set; }
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = C:\Users\Surface\Documents\Projects\TaskTracker\TaskTracker\TaskTracker\taskmanager.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndividualTask>().HasNoKey();
        }
        

    }
}

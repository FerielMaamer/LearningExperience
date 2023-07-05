using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Globalization;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace TaskTracker.Models
{
    public class TaskDbContext : DbContext
    {
        
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }
        public DbSet<IndividualTask> TasksGroup { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndividualTask>().HasKey(e => e.TaskId);
            modelBuilder.Entity<User>().HasKey(e => e.UserId);
            modelBuilder.Entity<Category>().HasKey(e => e.CatId);


        }


    }
}

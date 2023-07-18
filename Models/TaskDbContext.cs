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
        public DbSet<IndividualTask> IndividualTask { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndividualTask>().HasKey(e => e.TaskId);
            modelBuilder.Entity<User>().HasKey(e => e.UserId);
            modelBuilder.Entity<IndividualTask>().HasKey(e => e.CatId);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CatId = 1,
                    Name = "Exercice"
                },
                new Category
                {
                    CatId = 2,
                    Name = "Work"
                },
                new Category
                {
                    CatId = 3,
                    Name = "Errands"
                },
                new Category
                {
                    CatId = 4,
                    Name = "Study"
                },
                new Category
                {
                    CatId = 5,
                    Name = "Shopping"
                }
            );
        }


    }
}

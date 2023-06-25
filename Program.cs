using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskTracker.Data;
using TaskTracker.Models;
using Task = TaskTracker.Models.Task;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Create the database and perform operations
        CreateDatabase(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    static void CreateDatabase(IServiceCollection services)
    {
        // Connection string for SQLite in-memory database
        string connectionString = "Data Source=:memory:;Version=3;New=True;";

        // Register the DbContext using the SQLite connection string
        services.AddDbContext<TaskDbContext>(options =>
            options.UseSqlite(connectionString));

        // Build the service provider to resolve the DbContext
        using (var serviceProvider = services.BuildServiceProvider())
        {
            // Resolve the DbContext
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();

                // Ensure the database is created
                dbContext.Database.EnsureCreated();

                // Perform database operations
                // For example, add a new task
                /*var task = new OneTask
                {
                    Title = "Sample Task",
                    Description = "This is a sample task",
                    IsCompleted = false
                };
                dbContext.Tasks.Add(task);
                dbContext.SaveChanges();

                // Retrieve tasks from the database
                var tasks = dbContext.Tasks.ToList();
                foreach (var t in tasks)
                {
                    Console.WriteLine($"Task ID: {t.Id}");
                    Console.WriteLine($"Title: {t.Title}");
                    Console.WriteLine($"Description: {t.Description}");
                    Console.WriteLine($"IsCompleted: {t.IsCompleted}");
                    Console.WriteLine();
                }*/
            }
        }

        Console.WriteLine("Task Manager database created successfully.");
    }
}

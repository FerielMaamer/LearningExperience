using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskTracker.Data;
using TaskTracker.Models;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("Tasks") ?? "Data Source=taskmanager.db";

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlite(connectionString));

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

        var tasks = app.MapGroup("/api/students");

        /*app.MapGet("/", TaskAPIs.GetAllStudents);
        app.MapGet("/school/{school}", TaskAPIs.GeStudentsBySchool);
        app.MapGet("/{id}", TaskAPIs.GetStudentById);
        app.MapPost("/", TaskAPIs.InsertStudent);
        app.MapPut("/{id}", TaskAPIs.UpdateStudent);
        app.MapDelete("/{id}", TaskAPIs.DeleteStudent);*/

        app.Run();

    }
    
}

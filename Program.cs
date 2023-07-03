using Microsoft.EntityFrameworkCore;
using TaskTracker.Data;

class Program
{
    static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("Tasks") ?? "Data Source=./taskmanager.db";

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "MyAllowSpecificOrigins",
                        policy =>
                        {
                            policy.WithOrigins("https://localhost:8080")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .WithMethods("PUT", "DELETE", "GET", "POST");
                        });
        });


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
        app.UseCors(MyAllowSpecificOrigins);
        app.UseCors(options => {
            options.AllowAnyOrigin();
            options.AllowAnyMethod();
            options.AllowAnyHeader();

        });
        app.Run();

    }

}
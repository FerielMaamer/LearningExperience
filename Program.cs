global using TaskTracker.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Configuration;
using TaskTracker.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IdentityServer3.Core.Services;

class Program
{
    static void Main(string[] args)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        var builder = WebApplication.CreateBuilder(args);
        string connectionStrings = "Host=way-stoat-4471.g8z.cockroachlabs.cloud;Port=26257;Database=defaultdb;Username=ferielmaamer_yahoo;Password=IvfVlA4rL237b8MpUdYcgw;SslMode=VerifyFull";

        //might need to remove the below in the future
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

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IUserServices, UserService>();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<TaskDbContext>(options => options.UseNpgsql(connectionStrings));

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
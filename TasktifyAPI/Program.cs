
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TasktifyAPI.Models;
using TasktifyAPI.Repositories.Context;
using TasktifyAPI.Repositories.Contracts;
using TasktifyAPI.Repositories.Repositories;
using TasktifyAPI.Services.Contracts;
using TasktifyAPI.Services.Helpers;
using TasktifyAPI.Services.Services;

namespace TasktifyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Explicitly add appsettings.json to the configuration builder
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())  // Set base path to the current directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)  // Ensure appsettings.json is loaded
                .AddEnvironmentVariables();  // You can also load environment variables if needed

            // Log configuration values to ensure they are being loaded correctly
            Console.WriteLine("Reading Configuration Values from appsettings.json:");
            Console.WriteLine($"JWT Key: {builder.Configuration["Jwt:Key"]}");
            Console.WriteLine($"JWT Issuer: {builder.Configuration["Jwt:Issuer"]}");
            Console.WriteLine($"JWT Audience: {builder.Configuration["Jwt:Audience"]}");
            Console.WriteLine($"String Connection: {builder.Configuration["DefaultConnection"]}");

            // Add services to the container.

            builder.Services.AddDbContext<TasktifyContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            // allow cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowsAll", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            // Register custom services
            ConfigureServices(builder.Services);


            // Register services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITaskService, TaskService>();

            // Register repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();

            // Register helpers
            builder.Services.AddScoped<PasswordManager>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            app.UseCors("AllowsAll");
            app.UseCors();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();  // Serves the Swagger JSON at /swagger/v1/swagger.json
                app.UseSwaggerUI();
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TasktifyAPI V1");
                //    c.RoutePrefix = string.Empty;  // Serve Swagger UI at the root (i.e., https://localhost:7050/)
                //});
            }



            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();

            void ConfigureServices(IServiceCollection services)
            {
                //cors
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowsAll",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                });
            }
        }
    }
}

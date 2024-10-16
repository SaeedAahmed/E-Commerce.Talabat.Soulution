using AutoMapper;
using E_Commerce.APIs.Errors;
using E_Commerce.APIs.Extensions;
using E_Commerce.APIs.Helpers.Profiles;
using E_Commerce.APIs.Middleware;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Identity;
using E_Commerce.Repository.Identity.IdentityDbcontextSeed;
using E_Commerce.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commerce.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerService();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            builder.Services.AddAppService();
            builder.Services.IdentityServices();
            builder.Services.AddAuthentication();

            var app = builder.Build();

           using var Scope = app.Services.CreateScope();
            var Service= Scope.ServiceProvider;
            
            var LoggerFactory= Service.GetRequiredService<ILoggerFactory>();
            try
            {
                var _dbContext = Service.GetRequiredService<StoreContext>(); //Ask Clr Create obj from dbContext Explicitly
                await _dbContext.Database.MigrateAsync(); //Update Database
                await StoreContextSeed.SeedAsync(_dbContext); //Data seeding

                var _IdentityContext = Service.GetRequiredService<IdentityContext>(); //Ask Clr Create obj from dbContext Explicitly
                await _IdentityContext.Database.MigrateAsync(); //Update Database

                var userManger = Service.GetRequiredService<UserManager<AppUser>>();
                await IdentityContextSeed.SeedUserAsync(userManger);
            }
            catch (Exception ex)
            {

                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error has been occured during apply the migration");
            }

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

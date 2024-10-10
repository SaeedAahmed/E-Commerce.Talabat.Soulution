using AutoMapper;
using E_Commerce.APIs.Errors;
using E_Commerce.APIs.Helpers.Profiles;
using E_Commerce.APIs.Middleware;
using E_Commerce.Core.Repositories.Contract;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            builder.Services.AddAutoMapper(typeof(mappingProfile));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var error = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                                                               .SelectMany(P => P.Value.Errors)
                                                                                               .Select(E => E.ErrorMessage)
                                                                                               .ToArray();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = error
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            var app = builder.Build();

           using var Scope = app.Services.CreateScope();
            var Service= Scope.ServiceProvider;
            var _dbContext = Service.GetRequiredService<StoreContext>(); //Ask Clr Create obj from dbContext Explicitly
            var LoggerFactory= Service.GetRequiredService<ILoggerFactory>();
            try
            {
                _dbContext.Database.Migrate(); //Update Database
                await StoreContextSeed.SeedAsync(_dbContext); //Data seeding
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

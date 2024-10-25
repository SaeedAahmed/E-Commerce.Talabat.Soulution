using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Services.Contract;
using E_Commerce.Repository.Identity;
using E_Commerce.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce.APIs.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection IdentityServices(this IServiceCollection services , IConfiguration config)
        {
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
            })
          .AddEntityFrameworkStores<IdentityContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = config["JWT:ValidIssuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"])),
                        ValidateAudience = true,
                        ValidAudience = config["JWT:ValidAudience"],
                        ValidateLifetime = true,
                    };

                });
            return services;
        }
 
        
    }
}

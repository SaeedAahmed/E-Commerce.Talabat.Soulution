using E_Commerce.Core.Entities.Identity;
using E_Commerce.Repository.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.APIs.Extensions
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection IdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
            })
          .AddEntityFrameworkStores<IdentityContext>();

            return services;
        }
 
        
    }
}

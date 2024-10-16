using E_Commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Identity.IdentityDbcontextSeed
{
    public class IdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName="Saeed Ahmed",
                    Email="Saeed.Ahmed@gmail.com",
                    UserName="Saeed.Ahmed",
                    PhoneNumber="01050961200"
                };
                await userManager.CreateAsync(user);
            } 
        }
    }
}

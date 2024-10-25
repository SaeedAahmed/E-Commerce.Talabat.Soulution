using E_Commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce.APIs.Extensions
{
    public static class UserMangerExtension
    {
        public static async Task<AppUser?> FindUserAddressByEmailAsync(this UserManager<AppUser> userManger , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManger.Users
                .Include(u=>u.Address)
                .FirstOrDefaultAsync(u=>u.Email == email);

            return  user;
        }
    }
}

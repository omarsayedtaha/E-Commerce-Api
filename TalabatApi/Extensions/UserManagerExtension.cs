using CoreLayer.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace TalabatApi.Extensions
{
    public static class  UserManagerExtension
    {
        public static async Task<AppUser>  GetUserWithAddressAsync(this UserManager<AppUser> userManager , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 

            var user = await userManager.Users.Include(U=>U.UserAddress).FirstOrDefaultAsync(U=>U.Email==email);

            return user ; 
        }
    }
}

using CoreLayer.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Userdata
{
    public class AppUserDataSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
               var user = new AppUser()
                    {
                        DisplayName = "Omar Sayed",
                        Email = "os.taha007@gmail.com",
                        UserName = "omar.sayed",
                        PhoneNumber = "01122334455"
                    };

                await userManager.CreateAsync(user, "Pa$$w0rd");

            }
        }
    }
}

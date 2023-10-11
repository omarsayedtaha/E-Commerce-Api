using CoreLayer.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface ItokenService
    {
        public Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManager);
    }
}

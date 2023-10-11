using CoreLayer.Entities.IdentityModule;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class TokenService : ItokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManager)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier , User.DisplayName),
                new Claim(ClaimTypes.Email, User.Email) ,
            };

            var UserRoles = await userManager.GetRolesAsync(User);
            foreach (var role in UserRoles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            var Token = new JwtSecurityToken(
              issuer: _config["JWT:ValidIssuer"],
              audience: _config["JWT:ValidAudience"],
              expires: DateTime.Now.AddDays(double.Parse(_config["JWT:DurationInDays"])),
              claims: authClaims,
              signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
              );

            return  new JwtSecurityTokenHandler().WriteToken(Token);
            
        }
    }
}

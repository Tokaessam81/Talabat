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
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _config;

        public AuthServices(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager)
        {
            var Privateclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email ,user.Email!),
            };
            var UserRole = await userManager.GetRolesAsync(user);
            foreach (var role in UserRole)
            {
                Privateclaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:AuthKey"]??string.Empty));
            var Token = new JwtSecurityToken(
            audience: _config["JWT:Audience"]
            , issuer: _config["JWT:Issure"]
            , expires: DateTime.Now.AddDays(double.Parse(_config["JWT:expireDate"] ?? "0"))
            , claims:Privateclaims
            , signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
               
        }
    }
}

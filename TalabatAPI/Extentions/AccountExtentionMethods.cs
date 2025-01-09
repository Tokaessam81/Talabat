using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;
using TalabatAPI.DTO;

namespace TalabatAPI.Extentions
{
    public static class AccountExtentionMethods
    {
        public async static Task<AppUser> FindAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal user)
        {
            var UserByEmail=user.FindFirstValue(ClaimTypes.Email);
            var UserwithAddress = await userManager.Users.Include(u=>u.address).FirstOrDefaultAsync(u=>u.NormalizedEmail==UserByEmail.ToUpper());

            return UserwithAddress;
        }
    }
}

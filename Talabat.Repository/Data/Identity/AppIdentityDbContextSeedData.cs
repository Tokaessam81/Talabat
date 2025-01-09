using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Data.Identity
{
    public static class AppIdentityDbContextSeedData
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var User= new AppUser()
                {
                    DisplayName="Toka Essam",
                    Email="Tokaessam12@gmail.com",
                    PhoneNumber="010123456789",
                    UserName="Tokaessam"
                    
                };
               await _userManager.CreateAsync(User, "Pa$$w0rd");
            }
        }
    }
}

using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        
        {
            var defaultUser = new ApplicationUser { UserName = "Test@grapecity.com", Email = "Test@grapecity.com" };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
             var result =   await userManager.CreateAsync(defaultUser, "Test@1234");
            }
        }
    }
}

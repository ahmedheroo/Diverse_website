using Diverse_website.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Data
{
    public class DBIntializer
    {
        public static void seed(UserManager<IdentityUser> usermanager)
        {
            if (usermanager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };

                IdentityResult result = usermanager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    usermanager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}

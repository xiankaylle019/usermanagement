using System;
using System.Linq;
using System.Threading.Tasks;
using ClientAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClientAPI.Bootstrapper
{
    public class RoleSeeder
    {
        private readonly IServiceProvider _sProvider;
        public RoleSeeder (IServiceProvider sProvider) {

            _sProvider = sProvider;

        }
        public async Task SeedRoles () {

            var roleManager = _sProvider.GetRequiredService<RoleManager<IdentityRole>> ();
           
            string[] roleNames = { "Admin", "User", "Member","Manager" };      

            var role = await roleManager.FindByNameAsync("Admin");
            
            if(role == null){

                foreach (var roleName in roleNames) {

                    if (!(await roleManager.RoleExistsAsync (roleName)))
                        await roleManager.CreateAsync (new IdentityRole { Name = roleName });

                }
            }
          
        }

    }
}
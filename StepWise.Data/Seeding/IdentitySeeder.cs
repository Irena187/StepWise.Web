using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client.Extensibility;
using StepWise.Data.Models;
using StepWise.Data.Seeding.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StepWise.Common.ApplicationConstants;

namespace StepWise.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        private  readonly string[] DefaultRoles 
            = { AdminRoleName, CreatorRoleName, UserRoleName};

        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;
        private readonly IUserEmailStore<ApplicationUser> emailStore;

        public IdentitySeeder(
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public async Task SeedIdentityAsync()
        {
            await this.SeedRolesAsync();
            await this.SeedUsersAsync(); 
        }
         
        private async Task SeedRolesAsync()
        {
            foreach (string defaultRole in DefaultRoles)
            {
                bool roleExists = await this.roleManager
                    .RoleExistsAsync(defaultRole);
                if (!roleExists)
                {
                    IdentityRole<Guid> newRole = new IdentityRole<Guid>(defaultRole);
                    IdentityResult result = await this.roleManager
                        .CreateAsync(newRole);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Exception while seeding the role");
                    }
                }
            }
        }

        private async Task SeedUsersAsync()
        {
            await SeedUserAsync("testUser@stepWise.com", "User@1234", UserRoleName);
            await SeedUserAsync("testCreator@stepWise.com", "Creator@1234", CreatorRoleName);
            await SeedUserAsync("testAdmin@stepWise.com", "Admin@1234", AdminRoleName);
        }

        private async Task SeedUserAsync(string email, string password, string role)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser != null) return;

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new Exception($"Failed to create {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");

            var roleResult = await userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
                throw new Exception($"Failed to assign role to {email}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
        }

    }
}

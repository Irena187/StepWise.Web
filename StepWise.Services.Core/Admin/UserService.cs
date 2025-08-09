using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StepWise.Data.Models;
using StepWise.Services.Core.Admin.Interfaces;
using StepWise.Web.ViewModels.Admin.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core.Admin
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        // Returns a list of users for the admin panel, excluding the current admin
        public async Task<IEnumerable<UserManagementIndexViewModel>> GetUserManagementBoardDataAsync(string userId)
        {
            IEnumerable<UserManagementIndexViewModel> users = await this
                .userManager
                .Users
                .Where(u => u.Id.ToString().ToLower() != userId.ToLower() && !u.IsDeleted)
                .Select(u => new UserManagementIndexViewModel
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    Roles = this.userManager.GetRolesAsync(u)
                        .GetAwaiter()
                        .GetResult() 
                })
                .ToArrayAsync();

            return users;
        }

        // Assigns a role to a user if they don’t already have it
        public async Task<bool> AssignRoleAsync(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var roles = await userManager.GetRolesAsync(user);
            if (roles.Contains(role))
                return false;

            var result = await userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        // Soft deletes a user (marks them as deleted) or locks them out if soft delete isn’t supported
        public async Task<bool> SoftDeleteUserAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return false;


            var isSoftDeleteSupported = user.GetType().GetProperty("IsDeleted") != null;
            if (isSoftDeleteSupported)
            {
                user.GetType().GetProperty("IsDeleted").SetValue(user, true);
                var updateResult = await userManager.UpdateAsync(user);
                return updateResult.Succeeded;
            }
            else
            {
                var lockoutEnd = DateTimeOffset.MaxValue;
                var result = await userManager.SetLockoutEndDateAsync(user, lockoutEnd);
                return result.Succeeded;
            }
        }

    }
}

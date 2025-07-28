using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StepWise.Data.Models;
using StepWise.Services.Core.Admin.Interfaces;
using StepWise.Web.ViewModels.Admin.UserManagement;

namespace StepWise.Web.Areas.Admin.Controllers
{
    public class UserManagementController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagementController(IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<UserManagementIndexViewModel> users = await this
                .userService
                .GetUserManagementBoardDataAsync(this.GetUserId().ToString());
            return this.View(users);
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrWhiteSpace(role))
            {
                return BadRequest("Invalid user or role.");
            }

            var currentRoles = await userManager.GetRolesAsync(user);

            var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove existing roles.");
                return RedirectToAction(nameof(Index));
            }

            var addResult = await userManager.AddToRoleAsync(user, role);
            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to assign the new role.");
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Invalid user.";
                return RedirectToAction(nameof(Index));
            }

            var result = await userService.SoftDeleteUserAsync(id);

            if (result)
            {
                TempData["SuccessMessage"] = "User deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete user.";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using StepWise.Services.Core.Admin.Interfaces;
using StepWise.Web.ViewModels.Admin.UserManagement;

namespace StepWise.Web.Areas.Admin.Controllers
{
    public class UserManagementController : BaseAdminController
    {
        private readonly IUserService userService;

        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<UserManagementIndexViewModel> users = await this
                .userService
                .GetUserManagementBoardDataAsync(this.GetUserId().ToString());
            return this.View(users);
        }
    }
}

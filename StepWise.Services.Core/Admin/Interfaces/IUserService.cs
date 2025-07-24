using StepWise.Web.ViewModels.Admin.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core.Admin.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserManagementIndexViewModel>> GetUserManagementBoardDataAsync(string userId);
        Task<bool> AssignRoleAsync(string userId, string role);
        Task<bool> SoftDeleteUserAsync(string userId);

    }
}

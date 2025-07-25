using StepWise.Web.ViewModels.Admin.CareerPathManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core.Admin.Interfaces
{
    public interface ICareerPathAdminService
    {
        Task<IEnumerable<CareerPathAdminViewModel>> GetAllCareerPathsAsync();
        Task SoftDeleteAsync(Guid id);
        Task RestoreAsync(Guid id);
    }

}

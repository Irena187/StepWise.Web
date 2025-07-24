using StepWise.Data.Models;
using StepWise.Web.ViewModels.CareerPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core.Interfaces
{
    public interface ICareerPathService
    {
        Task<IEnumerable<AllCareerPathsIndexViewModel>> GetAllCareerPathsAsync();

        Task<CareerPathDetailsViewModel?> GetCareerPathByIdAsync(Guid id);

        Task<bool> CreateCareerPathAsync(AddCareerPathInputModel inputModel, Guid userId);

        Task<EditCareerPathInputModel?> GetCareerPathForEditAsync(Guid id, Guid userId);

        Task<bool> UpdateCareerPathAsync(EditCareerPathInputModel inputModel, Guid userId);

        Task<CareerPath?> GetCareerPathForDeleteAsync(Guid id, Guid userId);

        Task<bool> DeleteCareerPathAsync(Guid id, Guid userId);

        Task<IEnumerable<AllCareerPathsIndexViewModel>> GetCareerPathsByCreatorUserIdAsync(Guid userId);

        Task<List<Guid>> GetCompletedStepIdsForUserAsync(Guid userId, Guid careerPathId);

        Task UpdateCareerPathIsActiveStatusForUserAsync(Guid userId);

        Task MarkStepCompletedAsync(Guid userId, Guid stepId);

        Task<PagedCareerPathsViewModel> GetPagedCareerPathsAsync(int page, int pageSize);

    }
}

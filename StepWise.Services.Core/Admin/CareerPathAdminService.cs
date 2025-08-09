using StepWise.Data.Models;
using StepWise.Services.Core.Admin.Interfaces;
using StepWise.Web.ViewModels.Admin.CareerPathManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StepWise.Data.Repository.Interfaces;
using Nest;
using StepWise.Data.Repository;

namespace StepWise.Services.Core.Admin
{
    public class CareerPathAdminService : ICareerPathAdminService
    {
        private readonly ICareerPathRepository careerPathRepository;

        public CareerPathAdminService(ICareerPathRepository careerPathRepository)
        {
            this.careerPathRepository = careerPathRepository;
        }

        // Returns all non-deleted career paths for admin viewing
        public async Task<IEnumerable<CareerPathAdminViewModel>> GetAllCareerPathsAsync()
        {
            var allPaths = await careerPathRepository.GetAllAsync();

            return allPaths
                .Where(cp => !cp.IsDeleted)
                .Select(cp => new CareerPathAdminViewModel
                {
                    Id = cp.Id,
                    Title = cp.Title,
                    Description = cp.Description,
                    IsPublic = cp.IsPublic,
                    StepsCount = cp.Steps?.Count(s => !s.IsDeleted) ?? 0,
                    IsDeleted = cp.IsDeleted
                });
        }

        // Marks a career path as deleted without actually removing it from the database
        public async Task SoftDeleteAsync(Guid id)
        {
            var path = await careerPathRepository.GetByIdAsync(id);

            if (path == null || path.IsDeleted)
            {
                throw new InvalidOperationException("Career path not found or already deleted.");
            }

            path.IsDeleted = true;
            await careerPathRepository.UpdateAsync(path);
            await careerPathRepository.SaveChangesAsync();
        }

        // Reverses a soft delete — makes a previously deleted career path active again
        public async Task RestoreAsync(Guid id)
        {
            var path = await careerPathRepository.GetByIdAsync(id);

            if (path == null || !path.IsDeleted)
            {
                throw new InvalidOperationException("Career path not found or not deleted.");
            }

            path.IsDeleted = false;
            await careerPathRepository.UpdateAsync(path);
            await careerPathRepository.SaveChangesAsync();
        }
    }

}

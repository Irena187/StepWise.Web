using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;
using StepWise.Data.Repository.Interfaces;
using StepWise.Services.Core.Interfaces;
using StepWise.Web.ViewModels.CareerPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Services.Core
{
    public class CareerPathService : ICareerPathService
    {
        private IRepository<CareerPath, Guid> careerPathRepository;
        public CareerPathService(IRepository<CareerPath, Guid> careerPathRepository)
        {
            this.careerPathRepository = careerPathRepository;
        }

        public Task<IEnumerable<AllCareerPathsIndexViewModel>> GetAllCareerPathsAsync()
        {
            IEnumerable<AllCareerPathsIndexViewModel> allCareerPaths = this.dbContext
                .CareerPaths
                .Select(cp => new AllCareerPathsIndexViewModel()
                {
                    Id = cp.Id,
                    Title = cp.Title,
                    Description = cp.Description,
                    GoalProfession = cp.GoalProfession.Name,
                    IsPublic = cp.IsPublic,
                    CreatedByUserName = cp.CreatedByUser?.UserName,
                    StepsCount = cp.Steps.Count(),
                    CreatedDate = cp.CreatedOn
                })
        }

        public Task<CareerPathDetailsViewModel> GetCareerPathDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AllCareerPathsIndexViewModel>> IndexGetAllCareerPathsAsync()
        {
            var careerPaths = await this.careerPathRepository
                           .GetAllAttached()
                           .Include(cp => cp.User)
                           .Include(cp => cp.Steps)
                           .Where(cp => cp.IsPublic) // Or apply filtering logic
                           .Select(cp => new AllCareerPathsIndexViewModel
                           {
                               Id = cp.Id,
                               Title = cp.Title,
                               Description = cp.Description,
                               GoalProfession = cp.GoalProfession,
                               IsPublic = cp.IsPublic,
                               CreatedByUserName = cp.User.UserName,
                               StepsCount = cp.Steps.Count,
                           })
                           .OrderBy(cp => cp.Title)
                           .ToArrayAsync();
        }
    }
}

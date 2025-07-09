using Microsoft.EntityFrameworkCore;
using StepWise.Data;
using StepWise.Data.Models;
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
        private readonly StepWiseDbContext dbContext;
        public CareerPathService(StepWiseDbContext dbContext)
        {
            this.dbContext = dbContext;   
        }

        public async Task<IEnumerable<AllCareerPathsIndexViewModel>> GetAllCareerPathsAsync()
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
    }
}

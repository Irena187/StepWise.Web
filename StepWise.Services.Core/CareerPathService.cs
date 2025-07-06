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

        public Task<IEnumerable<AllCareerPathsIndexViewModel>> GetAllCareerPathsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

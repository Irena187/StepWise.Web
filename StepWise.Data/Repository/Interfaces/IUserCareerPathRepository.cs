using StepWise.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Repository.Interfaces
{
    public interface IUserCareerPathRepository 
        : IRepository<UserCareerPath, Guid>,
        IAsyncRepository<UserCareerPath, Guid>
    {
        Task<List<UserCareerPath>> GetActiveByUserIdWithCareerPathStepsAsync(Guid userId);
    }
}

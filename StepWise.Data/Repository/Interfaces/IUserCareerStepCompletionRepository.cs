using StepWise.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Repository.Interfaces
{
    public interface IUserCareerStepCompletionRepository 
        : IRepository<UserCareerStepCompletion, Guid>,
        IAsyncRepository<UserCareerStepCompletion, Guid>
    {
        Task<int> CountCompletedStepsAsync(Guid userId, Guid careerPathId);
        Task<bool> ExistsAsync(Guid userId, Guid stepId);
        Task<List<Guid>> GetCompletedStepIdsAsync(Guid userId, Guid careerPathId);
    }

}

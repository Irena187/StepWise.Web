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
        Task<IEnumerable<AllCareerPathsIndexViewModel>> IndexGetAllCareerPathsAsync();
        Task AddCareerPathAsync(AddCareerPathInputModel model);
        Task<CareerPathDetailsViewModel> GetCareerPathDetailsAsync(Guid id);
    } 
}

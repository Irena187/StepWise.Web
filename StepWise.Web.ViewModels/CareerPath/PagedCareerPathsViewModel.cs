using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Web.ViewModels.CareerPath
{
    public class PagedCareerPathsViewModel
    {
        public IEnumerable<AllCareerPathsIndexViewModel> CareerPaths { get; set; } = new List<AllCareerPathsIndexViewModel>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }


}

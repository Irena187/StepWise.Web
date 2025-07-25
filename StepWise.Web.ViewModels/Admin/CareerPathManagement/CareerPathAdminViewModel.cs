using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Web.ViewModels.Admin.CareerPathManagement
{
    public class CareerPathAdminViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsPublic { get; set; }
        public int StepsCount { get; set; }
        public bool IsDeleted { get; set; } 
    }


}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
           Id = Guid.NewGuid();
        }

        // Navigation properties for many-to-many relationships

        [Comment("Career paths created by this user")]
        public virtual ICollection<CareerPath> CreatedCareerPaths { get; set; } = new List<CareerPath>();

        [Comment("Career paths this user bookmarked")]
        public virtual ICollection<UserCareerPath> FollowedCareerPaths { get; set; } = new List<UserCareerPath>();
    }
}

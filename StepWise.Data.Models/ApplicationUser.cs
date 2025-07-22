using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace StepWise.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        public virtual Creator? Creator { get; set; }

        // Navigation properties
        public virtual ICollection<UserCareerPath> FollowedCareerPaths { get; set; }
            = new HashSet<UserCareerPath>();
    }
}
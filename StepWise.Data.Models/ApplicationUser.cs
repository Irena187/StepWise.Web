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

        public bool IsDeleted { get; set; } = false;

        public virtual Creator? Creator { get; set; }

        public virtual ICollection<UserCareerPath> FollowedCareerPaths { get; set; }
            = new HashSet<UserCareerPath>();
    }
}
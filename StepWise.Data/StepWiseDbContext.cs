using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StepWise.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data
{
    public class StepWiseDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public StepWiseDbContext()
        {
            
        }
        public StepWiseDbContext(DbContextOptions<StepWiseDbContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<CalendarTask> CalendarTasks { get; set; } = null!;

        public virtual DbSet<CareerPath> CareerPaths { get; set; } = null!;

        public virtual DbSet<CareerStep> CareerSteps { get; set; } = null!;

        public virtual DbSet<Note> Notes { get; set; } = null!;

        public virtual DbSet<Profession> Professions { get; set; } = null!;

        public virtual DbSet<Skill> Skills { get; set; } = null!;
    }
}

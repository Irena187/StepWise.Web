using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepWise.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepWise.Data.Configuration
{
    public class CareerPathConfiguration : IEntityTypeConfiguration<CareerPath>
    {
        public void Configure(EntityTypeBuilder<CareerPath> builder)
        {
            
        }
    }
}

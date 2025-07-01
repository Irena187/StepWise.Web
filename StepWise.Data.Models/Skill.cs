using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StepWise.Common.EntityValidationConstants.Skill;

namespace StepWise.Data.Models
{
    [Comment("Skill in a profession")]
    public class Skill
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Comment("The professions this skill is in.")]
        public ICollection<Profession> Professions { get; set; } = new List<Profession>();
    }
}

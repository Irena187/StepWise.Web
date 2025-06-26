using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StepWise.Common.EntityValidationConstants.CalendarTask;

namespace StepWise.Data.Models
{
    [Comment("Calendar tasks that the user added to their calendar of events or deadlines")]
    public class CalendarTask
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Comment("The user that added the task to his calendar")]
        public ApplicationUser? User { get; set; }
    }
}

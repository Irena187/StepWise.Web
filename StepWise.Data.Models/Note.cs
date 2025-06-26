using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StepWise.Common.EntityValidationConstants.Note;

namespace StepWise.Data.Models
{
    [Comment("Notes that users can write to themselves to keep track of what they have completed.")]
    public class Note
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [MaxLength(ContentMaxLength)]
        public string? Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Comment("The user that wrote that note")]
        public ApplicationUser? User { get; set; }
    }
}

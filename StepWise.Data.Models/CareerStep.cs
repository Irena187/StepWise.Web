using System;

namespace StepWise.Data.Models
{
    public enum StepType
    {
        Course,
        Book,
        Internship,
        Job,
        Certification,
        Degree,
        Documentation,
        Other
    }

    public class CareerStep
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public StepType Type { get; set; }
        public string? Url { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? Deadline { get; set; }
        public Guid CareerPathId { get; set; }
        public CareerPath? CareerPath { get; set; }
        public bool IsDeleted { get; set; }
    }
}
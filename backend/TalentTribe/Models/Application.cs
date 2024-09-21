using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [ForeignKey("JobSeekerProfile")]
        public int JobSeekerProfileId { get; set; }
        public JobSeekerProfile? JobSeekerProfile { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }
        public Job? Job { get; set; }

        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        [Required, StringLength(50)]
        public string? Status { get; set; }

        [StringLength(255)]
        public string? ResumeUrl { get; set; }

        public string? CoverLetter { get; set; }

        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        public DateTime? InterviewDate { get; set; }

        public string? Feedback { get; set; }

        // New Navigation Property
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }
}

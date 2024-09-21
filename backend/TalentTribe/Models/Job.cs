using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [ForeignKey("EmployerProfile")]
        public int EmployerProfileId { get; set; }
        public EmployerProfile? EmployerProfile { get; set; }

        [Required, StringLength(255)]
        public string? JobTitle { get; set; }

        [Required]
        public string? JobDescription { get; set; }
        public string?companyName { get; set; }

        public string? RequiredSkills { get; set; }

        [StringLength(50)]
        public string? EmploymentType { get; set; }

        [StringLength(50)]
        public string? SalaryRange { get; set; }

        [StringLength(255)]
        public string? Location { get; set; }

        [StringLength(50)]
        public string? JobStatus { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;

        public DateTime? ApplicationDeadline { get; set; }

        public bool IsActive { get; set; } = true;

        [StringLength(50)]
        public string? ExperienceLevel { get; set; }
        public string? CompanyPictureUrl { get; set; }

        public ICollection<Application>? Applications { get; set; }
    }
}

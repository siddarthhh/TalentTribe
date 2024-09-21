using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class JobSeekerProfile
    {
        [Key]
        public int JobSeekerProfileId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ StringLength(255)]
        public string? FullName { get; set; }

        public string? Skills { get; set; }

        public string? Experience { get; set; }

        public string? Education { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(10)]
        public string? PostalCode { get; set; }

        [StringLength(255)]
        public string? ResumeUrl { get; set; }

        [StringLength(255)]
        public string? ProfilePictureUrl { get; set; }

        public ICollection<Credential>? Credentials { get; set; }
        public ICollection<Application>? Applications {  get; set; }
        public ICollection<JobSearchLog>? JobSearchLogs { get; set; }
    }
}

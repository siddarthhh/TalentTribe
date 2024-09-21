using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class Credential
    {
        [Key]
        public int CredentialId { get; set; }

        [ForeignKey("JobSeekerProfile")]
        public int JobSeekerProfileId { get; set; }
        public JobSeekerProfile? JobSeekerProfile { get; set; }

        [Required, StringLength(150)]
        public string? CredentialName { get; set; }

        [StringLength(255)]
        public string? CredentialUrl { get; set; }

        [StringLength(150)]
        public string? IssuedBy { get; set; }

        public DateTime? IssueDate { get; set; }

        [StringLength(50)]
        public string? CredentialType { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentTribe.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [ForeignKey("EmployerProfile")]
        public int EmployerProfileId { get; set; }
        public EmployerProfile? EmployerProfile { get; set; }

        [Required, StringLength(255)]
        public string? CompanyName { get; set; }

        public string? CompanyDescription { get; set; }

        [StringLength(255)]
        public string? Industry { get; set; }

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
        public string? WebsiteUrl { get; set; }

        [StringLength(255)]
        public string? ContactEmail { get; set; }

        [StringLength(15)]
        public string? ContactPhone { get; set; }
        public string? CompanyPictureUrl { get; set; }
        public ICollection<EmployerProfile>? EmployerProfiles { get; set; }
    }
}

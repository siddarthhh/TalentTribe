    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    namespace TalentTribe.Models
    {
        public class EmployerProfile
        {
            [Key]
            public int EmployerProfileId { get; set; }

            [ForeignKey("User")]
            public int UserId { get; set; }
            public User? User { get; set; }

            [StringLength(255)]
            public string? FullName { get; set; }

            [StringLength(255)]
            public string? PositionTitle { get; set; }

            [StringLength(255)]
            public string? Department { get; set; }

            [StringLength(255)]
            public string? WorkEmail { get; set; }

            [StringLength(15)]
            public string? WorkPhone { get; set; }

            public DateTime DateJoined { get; set; } = DateTime.Now;

            [StringLength(255)]

            public ICollection<Job>? Jobs { get; set; }
            // Navigation property to Company
            public Company? Company { get; set; }

        }
    }

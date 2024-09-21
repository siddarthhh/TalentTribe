using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalentTribe.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(255)]
        public string? Username { get; set; }

        [Required]
        public string ?PasswordHash { get; set; }

        [Required, StringLength(50)]
        public string? Role { get; set; }

        [Required, StringLength(255)]
        public string? Email { get; set; }

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? LastLogin { get; set; }

        public ICollection<JobSeekerProfile>? JobSeekerProfiles { get; set; }
        public ICollection<EmployerProfile>? EmployerProfiles { get; set; }
        public ICollection<Admin>? Admins { get; set; }
        public ICollection<Communication>? SentCommunications { get; set; }
        public ICollection<Communication>? ReceivedCommunications { get; set; }
    }
}

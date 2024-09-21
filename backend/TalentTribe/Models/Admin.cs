using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [StringLength(50)]
        public string? AdminRole { get; set; }

        [StringLength(255)]
        public string? ProfilePictureUrl { get; set; }

        public ICollection<AuditLog>? AuditLogs { get; set; }
        public ICollection<Report>? Reports { get; set; }
    }
}

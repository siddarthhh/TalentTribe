using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin? Admin { get; set; }

        [Required, StringLength(255)]
        public string? Action { get; set; }

        [StringLength(255)]
        public string? TargetEntity { get; set; }

        public int? TargetEntityId { get; set; }

        public DateTime ActionDate { get; set; } = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required, StringLength(255)]
        public string? ReportType { get; set; }

        public string? ReportContent { get; set; }

        [ForeignKey("Admin")]
        public int GeneratedBy { get; set; }
        public Admin? Admin { get; set; }

        public DateTime GeneratedDate { get; set; } = DateTime.Now;
    }
}

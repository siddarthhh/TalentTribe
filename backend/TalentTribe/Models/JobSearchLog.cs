using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class JobSearchLog
    {
        [Key]
        public int SearchLogId { get; set; }

        [ForeignKey("JobSeekerProfile")]
        public int JobSeekerProfileId { get; set; }
        public JobSeekerProfile? JobSeekerProfile { get; set; }

        public string? SearchQuery { get; set; }

        public DateTime SearchDate { get; set; } = DateTime.Now;

        public int? ResultsCount { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class Interview
    {
        [Key]
        public int InterviewId { get; set; }

        [ForeignKey("Application")]
        public int ApplicationId { get; set; }
        public Application? Application { get; set; }

        public DateTime InterviewDate { get; set; }

        [StringLength(50)]
        public string? InterviewType { get; set; }

        [StringLength(255)]
        public string? InterviewLink { get; set; }

        [StringLength(255)]
        public string? InterviewLocation { get; set; }

        public string? InterviewFeedback { get; set; }
    }
}

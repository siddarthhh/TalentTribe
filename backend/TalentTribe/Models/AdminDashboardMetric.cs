using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class AdminDashboardMetric
    {
        [Key]
        public int MetricId { get; set; }

        [Required, StringLength(255)]
        public string? MetricName { get; set; }

        public int MetricValue { get; set; }

        public DateTime MetricDate { get; set; } = DateTime.Now;
    }
}

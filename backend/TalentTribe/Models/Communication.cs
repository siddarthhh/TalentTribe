using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TalentTribe.Models
{
    public class Communication
    {
        [Key]
        public int CommunicationId { get; set; }

        [ForeignKey("Sender")]
        public int SenderId { get; set; }
        public User? Sender { get; set; }

        [ForeignKey("Receiver")]
        public int? ReceiverId { get; set; }
        public User? Receiver { get; set; }

        [Required]
        public string? Content { get; set; }

        [StringLength(50)]
        public string? CommunicationType { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations;

namespace PatiYuva.Models
{
    public class AdoptionRequest
    {
        public int Id { get; set; }

        [Required]
        public int AnimalId { get; set; }

        [Required]
        public string RequesterId { get; set; }

        [Display(Name = "Talep Tarihi")]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        [Display(Name = "Durum")]
        public string Status { get; set; } = "Beklemede"; // Beklemede, Onaylandı, Reddedildi

        [Display(Name = "Mesaj")]
        public string? Message { get; set; }

        // Navigation properties
        public virtual Animal? Animal { get; set; } = null!;
        public virtual ApplicationUser? Requester { get; set; } = null!;
    }
}
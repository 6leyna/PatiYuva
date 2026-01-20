using System.ComponentModel.DataAnnotations;

namespace PatiYuva.Models
{
    public class Animal
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "İsim")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Tür")]
        public string Type { get; set; }

        [Display(Name = "Cins")]
        public string? Breed { get; set; }

        [Display(Name = "Yaş")]
        public int? Age { get; set; }

        [Display(Name = "Cinsiyet")]
        public string? Gender { get; set; }

        [Display(Name = "Aşı Durumu")]
        public bool IsVaccinated { get; set; } = false;

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Fotoğraf")]
        public string? PhotoPath { get; set; }

        [Display(Name = "Sahip")]
        public string OwnerId { get; set; }

        [Display(Name = "Telefon Numarası")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ApplicationUser? Owner { get; set; }
        public virtual ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();
    }
}
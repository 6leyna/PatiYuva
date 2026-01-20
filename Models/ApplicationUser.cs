using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PatiYuva.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Ad Soyad")]
        public string? FullName { get; set; }

        [Display(Name = "Telefon")]
        public override string? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Rolü")]
        public string? Role { get; set; } = "Sahiplenici"; // Default role

        // Navigation properties
        public virtual ICollection<Animal> OwnedAnimals { get; set; } = new List<Animal>();
        public virtual ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();
    }
}
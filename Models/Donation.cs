using System.ComponentModel.DataAnnotations;

namespace PatiYuva.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kullanıcı")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Hayvan")]
        public int AnimalId { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter uzunluğunda olabilir.")]
        public string DonorName { get; set; }

        [Required]
        [Display(Name = "E-posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string DonorEmail { get; set; }

        [Display(Name = "Telefon")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
        public string? DonorPhone { get; set; }

        [Required]
        [Display(Name = "Bağış Miktarı")]
        [Range(1, double.MaxValue, ErrorMessage = "Bağış miktarı 0'dan büyük olmalıdır.")]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Banka")]
        [StringLength(100, ErrorMessage = "Banka adı en fazla 100 karakter uzunluğunda olabilir.")]
        public string BankName { get; set; }

        [Required]
        [Display(Name = "IBAN")]
        [StringLength(34, MinimumLength = 26, ErrorMessage = "IBAN numarası 26-34 karakter uzunluğunda olmalıdır.")]
        [RegularExpression("^[A-Z]{2}[0-9]{2}[A-Z0-9]{4}[0-9]{7}([A-Z0-9]?){0,16}$", ErrorMessage = "Geçerli bir IBAN formatı girin.")]
        public string Iban { get; set; }

        [Display(Name = "Mesaj")]
        public string? Message { get; set; }

        [Display(Name = "Bağış Tarihi")]
        public DateTime DonationDate { get; set; } = DateTime.Now;

        [Display(Name = "Onay Durumu")]
        public bool IsConfirmed { get; set; } = false;

        // Navigation properties
        public virtual ApplicationUser? User { get; set; } = null!;
        public virtual Animal? Animal { get; set; } = null!;
    }
}
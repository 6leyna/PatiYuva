using System.ComponentModel.DataAnnotations;

namespace PatiYuva.Models.ViewModels
{
    public class DonationViewModel
    {
        public int AnimalId { get; set; }
        
        [Required(ErrorMessage = "Ad Soyad gereklidir.")]
        [Display(Name = "Ad Soyad")]
        [StringLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter uzunluğunda olabilir.")]
        public string DonorName { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir.")]
        [Display(Name = "E-posta")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        public string DonorEmail { get; set; }

        [Display(Name = "Telefon")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
        public string? DonorPhone { get; set; }

        [Required(ErrorMessage = "Bağış miktarı gereklidir.")]
        [Range(1, double.MaxValue, ErrorMessage = "Bağış miktarı 0'dan büyük olmalıdır.")]
        [Display(Name = "Bağış Miktarı (₺)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Banka bilgisi gereklidir.")]
        [Display(Name = "Banka")]
        [StringLength(100, ErrorMessage = "Banka adı en fazla 100 karakter uzunluğunda olabilir.")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "IBAN bilgisi gereklidir.")]
        [Display(Name = "IBAN")]
        [StringLength(34, MinimumLength = 26, ErrorMessage = "IBAN numarası 26-34 karakter uzunluğunda olmalıdır.")]
        [RegularExpression("^[A-Z]{2}[0-9]{2}[A-Z0-9]{4}[0-9]{7}([A-Z0-9]?){0,16}$", ErrorMessage = "Geçerli bir IBAN formatı girin.")]
        public string Iban { get; set; }

        [Display(Name = "Mesaj")]
        [StringLength(500, ErrorMessage = "Mesaj en fazla 500 karakter uzunluğunda olabilir.")]
        public string? Message { get; set; }

        // Animal details for display
        public string? AnimalName { get; set; }
        public string? AnimalPhotoPath { get; set; }
    }
}
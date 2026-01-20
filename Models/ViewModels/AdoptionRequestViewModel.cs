namespace PatiYuva.Models.ViewModels
{
    public class AdoptionRequestViewModel
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }
        public string AnimalPhotoPath { get; set; }
        public string? RequesterId { get; set; }
        public string? RequesterFullName { get; set; }
        public string? RequesterEmail { get; set; }
        public string? RequesterPhone { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public string? Message { get; set; }
    }
}
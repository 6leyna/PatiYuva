using Microsoft.AspNetCore.Http;

namespace PatiYuva.Models.ViewModels
{
    public class AnimalViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Breed { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public bool IsVaccinated { get; set; }
        public string? Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OwnerId { get; set; }
        public string? OwnerFullName { get; set; }
    }
}
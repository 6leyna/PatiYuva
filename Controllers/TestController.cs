using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PatiYuva.Data;
using PatiYuva.Models;

namespace PatiYuva.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TestController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddTestDonation()
        {
            // Get the first user from the database
            var user = await _context.Users.FirstOrDefaultAsync();
            if (user == null)
            {
                return Content("Kullanıcı bulunamadı. Lütfen önce bir kullanıcı oluşturun.");
            }

            // Get the first animal from the database
            var animal = await _context.Animals.FirstOrDefaultAsync();
            if (animal == null)
            {
                return Content("Hayvan bulunamadı. Lütfen önce bir hayvan ekleyin.");
            }

            // Create a test donation
            var donation = new Donation
            {
                UserId = user.Id,
                AnimalId = animal.Id,
                DonorName = "Test Kullanıcı",
                DonorEmail = "test@example.com",
                DonorPhone = "5551234567",
                Amount = 250.00m,
                BankName = "Ziraat Bankası",
                Iban = "TR33000100123456789012",
                Message = "Test bağışı - hayvanlarımıza destek olmak istedim.",
                DonationDate = DateTime.Now,
                IsConfirmed = true
            };

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            return Content($"Test bağışı başarıyla eklendi: {donation.DonorName}, {donation.Amount} TL");
        }
    }
}
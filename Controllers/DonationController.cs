using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatiYuva.Data;
using PatiYuva.Models;
using PatiYuva.Models.ViewModels;

namespace PatiYuva.Controllers
{
    [Route("[controller]")]
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Donation
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Redirect to MyDonations for authenticated users
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MyDonations");
            }
            
            // For non-authenticated users, show a list of animals they can donate to
            var animals = await _context.Animals.ToListAsync();
            return View(animals);
        }

        // GET: Donation/MakeDonation/5
        [HttpGet("MakeDonation/{animalId?}")]
        [Authorize]
        public async Task<IActionResult> MakeDonation(int? animalId)
        {
            if (animalId == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(animalId);
            if (animal == null)
            {
                return NotFound();
            }

            var viewModel = new DonationViewModel
            {
                AnimalId = animal.Id,
                AnimalName = animal.Name,
                AnimalPhotoPath = animal.PhotoPath
            };

            return View(viewModel);
        }

        // POST: Donation/MakeDonation/5
        [HttpPost("MakeDonation/{animalId?}")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeDonation(int? animalId, DonationViewModel model)
        {
            if (animalId.HasValue)
            {
                model.AnimalId = animalId.Value;
            }

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var animal = await _context.Animals.FindAsync(model.AnimalId);

                if (animal == null)
                {
                    return NotFound();
                }

                var donation = new Donation
                {
                    UserId = userId,
                    AnimalId = model.AnimalId,
                    DonorName = model.DonorName,
                    DonorEmail = model.DonorEmail,
                    DonorPhone = model.DonorPhone,
                    Amount = model.Amount,
                    BankName = model.BankName,
                    Iban = model.Iban,
                    Message = model.Message,
                    DonationDate = DateTime.Now,
                    IsConfirmed = false
                };

                _context.Donations.Add(donation);
                await _context.SaveChangesAsync();

                ViewBag.Message = $"{animal.Name} için {model.Amount} TL tutarında bağış yaptığınız için teşekkür ederiz!";

                // Yeni bağışı veritabanından al ve view'e gönder
                var newDonation = await _context.Donations
                    .Include(d => d.Animal)
                    .Where(d => d.UserId == userId && d.AnimalId == model.AnimalId && d.Id == donation.Id) // Use the created donation's ID
                    .FirstOrDefaultAsync();

                var viewModel = new DonationViewModel
                {
                    AnimalId = newDonation?.AnimalId ?? 0,
                    DonorName = newDonation?.DonorName ?? "",
                    DonorEmail = newDonation?.DonorEmail ?? "",
                    DonorPhone = newDonation?.DonorPhone ?? "",
                    Amount = newDonation?.Amount ?? 0,
                    BankName = newDonation?.BankName ?? "",
                    Iban = newDonation?.Iban ?? "",
                    Message = newDonation?.Message,
                    AnimalName = newDonation?.Animal?.Name ?? "Bilinmiyor",
                    AnimalPhotoPath = newDonation?.Animal?.PhotoPath ?? ""
                };

                return View("DonationSuccess", viewModel);
            }

            // If model state is not valid, reload the animal info
            var animalDetail = await _context.Animals.FindAsync(model.AnimalId);
            if (animalDetail != null)
            {
                model.AnimalName = animalDetail.Name;
                model.AnimalPhotoPath = animalDetail.PhotoPath;
            }

            return View(model);
        }

        // GET: Donation/MyDonations
        [HttpGet("MyDonations")]
        [Authorize]
        public async Task<IActionResult> MyDonations()
        {
            try
            {
                var userId = _userManager.GetUserId(User);

                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account");
                }

                var donations = await _context.Donations
                    .Include(d => d.Animal)
                    .Where(d => d.UserId == userId)
                    .OrderByDescending(d => d.DonationDate)
                    .ToListAsync();

                var viewModel = new List<DonationViewModel>();
                
                if (donations != null)
                {
                    foreach (var donation in donations)
                    {
                        if (donation != null)
                        {
                            viewModel.Add(new DonationViewModel
                            {
                                AnimalId = donation?.AnimalId ?? 0,
                                DonorName = donation?.DonorName ?? "",
                                DonorEmail = donation?.DonorEmail ?? "",
                                DonorPhone = donation?.DonorPhone ?? "",
                                Amount = donation?.Amount ?? 0,
                                BankName = donation?.BankName ?? "",
                                Iban = donation?.Iban ?? "",
                                Message = donation?.Message,
                                AnimalName = donation?.Animal?.Name ?? "Bilinmiyor",
                                AnimalPhotoPath = donation?.Animal?.PhotoPath ?? ""
                            });
                        }
                    }
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error in MyDonations: {ex.Message}");
                return View(new List<DonationViewModel>());
            }
        }

        // GET: Donation/AnimalDonations/5
        [HttpGet("AnimalDonations/{animalId?}")]
        public async Task<IActionResult> AnimalDonations(int? animalId)
        {
            if (animalId == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(animalId);
            if (animal == null)
            {
                return NotFound();
            }

            var donations = await _context.Donations
                .Include(d => d.User)
                .Where(d => d.AnimalId == animalId)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();

            ViewBag.AnimalName = animal.Name;
            ViewBag.TotalDonations = donations?.Sum(d => d.Amount) ?? 0;

            return View(donations);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatiYuva.Data;
using PatiYuva.Models;
using PatiYuva.Models.ViewModels;

namespace PatiYuva.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdoptionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Adoption/Request/5
        [Authorize(Roles = "Sahiplenici")]
        public async Task<IActionResult> CreateRequest(int? animalId)
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

            // Check if user already has a pending request for this animal
            var existingRequest = await _context.AdoptionRequests
                .Where(r => r.AnimalId == animalId && r.RequesterId == _userManager.GetUserId(User) && r.Status == "Beklemede")
                .FirstOrDefaultAsync();

            if (existingRequest != null)
            {
                ViewBag.Message = "Bu hayvan için zaten bir sahiplenme talebiniz bulunmaktadır.";
                return View("RequestResult");
            }

            ViewBag.AnimalName = animal.Name;
            ViewBag.AnimalId = animal.Id;

            return View();
        }

        // POST: Adoption/Request/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Sahiplenici")]
        public async Task<IActionResult> CreateRequest(int animalId, [Bind("Message")] AdoptionRequestViewModel model)
        {
            var animal = await _context.Animals.FindAsync(animalId);
            if (animal == null)
            {
                return NotFound();
            }

            // Check if user is trying to request their own animal
            var userId = _userManager.GetUserId(User);
            if (animal.OwnerId == userId)
            {
                ViewBag.Message = "Kendi hayvanınız için sahiplenme talebinde bulunamazsınız.";
                return View("RequestResult");
            }

            // Check if user already has a pending request for this animal
            var existingRequest = await _context.AdoptionRequests
                .Where(r => r.AnimalId == animalId && r.RequesterId == userId && r.Status == "Beklemede")
                .FirstOrDefaultAsync();

            if (existingRequest != null)
            {
                ViewBag.Message = "Bu hayvan için zaten bir sahiplenme talebiniz bulunmaktadır.";
                return View("RequestResult");
            }

            var request = new AdoptionRequest
            {
                AnimalId = animalId,
                RequesterId = userId,
                Message = model.Message,
                RequestDate = DateTime.Now,
                Status = "Beklemede"
            };

            _context.AdoptionRequests.Add(request);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Sahiplenme talebiniz başarıyla gönderildi. Lütfen onay bekleyin.";
            return View("RequestResult");
        }

        // GET: Adoption/MyRequests
        [Authorize(Roles = "Sahiplenici")]
        public async Task<IActionResult> MyRequests()
        {
            var userId = _userManager.GetUserId(User);
            
            var requests = await _context.AdoptionRequests
                .Include(r => r.Animal)
                .Where(r => r.RequesterId == userId)
                .OrderByDescending(r => r.RequestDate)
                .ToListAsync();

            var viewModel = requests.Select(r => new AdoptionRequestViewModel
            {
                Id = r.Id,
                AnimalId = r.AnimalId,
                AnimalName = r.Animal.Name,
                AnimalPhotoPath = r.Animal.PhotoPath,
                RequesterId = r.RequesterId,
                RequestDate = r.RequestDate,
                Status = r.Status,
                Message = r.Message
            }).ToList();

            return View(viewModel);
        }

        // GET: Adoption/MyAnimalsRequests
        [Authorize(Roles = "Sahip")]
        public async Task<IActionResult> MyAnimalsRequests()
        {
            var userId = _userManager.GetUserId(User);
            
            var requests = await _context.AdoptionRequests
                .Include(r => r.Animal)
                .Include(r => r.Requester)
                .Where(r => r.Animal.OwnerId == userId)
                .OrderByDescending(r => r.RequestDate)
                .ToListAsync();

            var viewModel = requests.Select(r => new AdoptionRequestViewModel
            {
                Id = r.Id,
                AnimalId = r.AnimalId,
                AnimalName = r.Animal.Name,
                AnimalPhotoPath = r.Animal.PhotoPath,
                RequesterId = r.RequesterId,
                RequesterFullName = r.Requester.FullName,
                RequesterEmail = r.Requester.Email,
                RequesterPhone = r.Requester.PhoneNumber,
                RequestDate = r.RequestDate,
                Status = r.Status,
                Message = r.Message
            }).ToList();

            return View(viewModel);
        }

        // POST: Adoption/UpdateRequestStatus
        [HttpPost]
        [Authorize(Roles = "Sahip")]
        public async Task<IActionResult> UpdateRequestStatus(int requestId, string status)
        {
            var request = await _context.AdoptionRequests
                .Include(r => r.Animal)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
            {
                return NotFound();
            }

            // Check if current user is the owner of the animal
            var userId = _userManager.GetUserId(User);
            if (request.Animal.OwnerId != userId)
            {
                return Forbid();
            }

            // Validate status
            if (status != "Onaylandı" && status != "Reddedildi")
            {
                return BadRequest();
            }

            request.Status = status;
            await _context.SaveChangesAsync();

            // If approved, we might want to send a notification or update the animal status
            if (status == "Onaylandı")
            {
                // Additional logic for adoption approval can be added here
            }

            return RedirectToAction(nameof(MyAnimalsRequests));
        }
    }
}
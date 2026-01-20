using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatiYuva.Data;
using PatiYuva.Models;
using PatiYuva.Models.ViewModels;
using System.IO;

namespace PatiYuva.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AnimalsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Animals
        public async Task<IActionResult> Index(string searchString, string animalType, int? minAge, int? maxAge)
        {
            var animals = from a in _context.Animals
                          select a;

            // Include owner information
            animals = animals.Include(a => a.Owner);

            // Filter by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(a => a.Name.Contains(searchString) || 
                                           a.Description.Contains(searchString) ||
                                           a.Type.Contains(searchString));
            }

            // Filter by animal type
            if (!string.IsNullOrEmpty(animalType))
            {
                animals = animals.Where(a => a.Type == animalType);
            }

            // Filter by age range
            if (minAge.HasValue)
            {
                animals = animals.Where(a => a.Age >= minAge);
            }

            if (maxAge.HasValue)
            {
                animals = animals.Where(a => a.Age <= maxAge);
            }

            var animalList = await animals.ToListAsync();

            // Convert to ViewModel
            var viewModel = animalList.Select(a => new AnimalViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Type = a.Type,
                Breed = a.Breed,
                Age = a.Age,
                Gender = a.Gender,
                IsVaccinated = a.IsVaccinated,
                Description = a.Description,
                PhotoPath = a.PhotoPath,
                PhoneNumber = a.PhoneNumber,
                OwnerFullName = a.Owner.FullName
            }).ToList();

            // Get distinct animal types for filter dropdown
            ViewBag.AnimalTypes = await _context.Animals.Select(a => a.Type).Distinct().ToListAsync();

            return View(viewModel);
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.Id == id.Value);

            if (animal == null)
            {
                return NotFound();
            }

            var viewModel = new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Breed = animal.Breed,
                Age = animal.Age,
                Gender = animal.Gender,
                IsVaccinated = animal.IsVaccinated,
                Description = animal.Description,
                PhotoPath = animal.PhotoPath,
                PhoneNumber = animal.PhoneNumber,
                OwnerFullName = animal.Owner?.FullName ?? "Bilinmiyor"
            };

            return View(viewModel);
        }

        // GET: Animals/Create
        [Authorize(Roles = "Sahip")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Sahip")]
        public async Task<IActionResult> Create([Bind("Name,Type,Breed,Age,Gender,IsVaccinated,Description,PhoneNumber,Photo")] AnimalViewModel animalViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                
                string fileName = null;
                if (animalViewModel.Photo != null)
                {
                    fileName = await UploadFile(animalViewModel.Photo);
                }

                var animal = new Animal
                {
                    Name = animalViewModel.Name,
                    Type = animalViewModel.Type,
                    Breed = animalViewModel.Breed,
                    Age = animalViewModel.Age,
                    Gender = animalViewModel.Gender,
                    IsVaccinated = animalViewModel.IsVaccinated,
                    Description = animalViewModel.Description,
                    PhoneNumber = animalViewModel.PhoneNumber,
                    PhotoPath = fileName,
                    OwnerId = userId,
                    CreatedDate = DateTime.Now
                };

                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalViewModel);
        }

        // GET: Animals/Edit/5
        [Authorize(Roles = "Sahip")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            // Check if the current user is the owner
            if (animal.OwnerId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            var viewModel = new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Breed = animal.Breed,
                Age = animal.Age,
                Gender = animal.Gender,
                IsVaccinated = animal.IsVaccinated,
                Description = animal.Description,
                PhotoPath = animal.PhotoPath
            };

            return View(viewModel);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Sahip")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Breed,Age,Gender,IsVaccinated,Description,PhoneNumber,Photo")] AnimalViewModel animalViewModel)
        {
            if (id != animalViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var animal = await _context.Animals.FindAsync(id);
                    if (animal == null)
                    {
                        return NotFound();
                    }

                    // Check if the current user is the owner
                    if (animal.OwnerId != _userManager.GetUserId(User))
                    {
                        return Forbid();
                    }

                    // Handle photo upload if provided
                    if (animalViewModel.Photo != null)
                    {
                        // Delete old photo if exists
                        if (!string.IsNullOrEmpty(animal.PhotoPath))
                        {
                            var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", animal.PhotoPath);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        animal.PhotoPath = await UploadFile(animalViewModel.Photo);
                    }

                    animal.Name = animalViewModel.Name;
                    animal.Type = animalViewModel.Type;
                    animal.Breed = animalViewModel.Breed;
                    animal.Age = animalViewModel.Age;
                    animal.Gender = animalViewModel.Gender;
                    animal.IsVaccinated = animalViewModel.IsVaccinated;
                    animal.Description = animalViewModel.Description;
                    animal.PhoneNumber = animalViewModel.PhoneNumber;

                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animalViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(animalViewModel);
        }

        // GET: Animals/Delete/5
        [Authorize(Roles = "Sahip")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            // Check if the current user is the owner
            if (animal.OwnerId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            var viewModel = new AnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type,
                Breed = animal.Breed,
                Age = animal.Age,
                Gender = animal.Gender,
                IsVaccinated = animal.IsVaccinated,
                Description = animal.Description,
                PhotoPath = animal.PhotoPath,
                OwnerFullName = animal.Owner.FullName
            };

            return View(viewModel);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Sahip")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                // Check if the current user is the owner
                if (animal.OwnerId != _userManager.GetUserId(User))
                {
                    return Forbid();
                }

                // Delete photo file if exists
                if (!string.IsNullOrEmpty(animal.PhotoPath))
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", animal.PhotoPath);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Animals.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }

        private async Task<string> UploadFile(IFormFile photo)
        {
            string fileName = null;
            if (photo != null && photo.Length > 0)
            {
                // Create uploads directory if not exists
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Generate unique filename
                fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
            }
            return fileName;
        }
    }
}
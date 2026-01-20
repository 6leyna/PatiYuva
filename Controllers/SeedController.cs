using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatiYuva.Data;
using PatiYuva.Models;
using System;
using System.Threading.Tasks;

namespace PatiYuva.Controllers
{
    [Route("seed")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("seed-animals")]
        public async Task<IActionResult> SeedAnimals()
        {
            // Create a default user if one doesn't exist
            var defaultUser = await _context.Users.FirstOrDefaultAsync();
            if (defaultUser == null)
            {
                return BadRequest("No user found. Please register a user first.");
            }

            // Check if animals already exist
            var existingAnimals = await _context.Animals.AnyAsync();
            if (existingAnimals)
            {
                return BadRequest("Animals already exist in the database.");
            }

            // Create 3 cats
            var cats = new[]
            {
                new Animal
                {
                    Name = "Mırmır",
                    Type = "Kedi",
                    Breed = "Van Kedisi",
                    Age = 2,
                    Gender = "Dişi",
                    IsVaccinated = true,
                    Description = "Çok sevimli ve dost canlısı Van kedisi. Herkesle hemen arkadaş olur.",
                    PhotoPath = "cat1.jpg", // Use the filename you added
                    OwnerId = defaultUser.Id,
                    PhoneNumber = "5551234567",
                    CreatedDate = DateTime.Now
                },
                new Animal
                {
                    Name = "Pamuk",
                    Type = "Kedi",
                    Breed = "Siyam Kedisi",
                    Age = 1,
                    Gender = "Erkek",
                    IsVaccinated = true,
                    Description = "Oyuncu ve enerjik Siyam kedisi. Özellikle çocuklara çok düşkündür.",
                    PhotoPath = "cat2.jpg", // Use the filename you added
                    OwnerId = defaultUser.Id,
                    PhoneNumber = "5551234568",
                    CreatedDate = DateTime.Now
                },
                new Animal
                {
                    Name = "Boncuk",
                    Type = "Kedi",
                    Breed = "Tekir",
                    Age = 3,
                    Gender = "Dişi",
                    IsVaccinated = true,
                    Description = "Sakin ve sevgi dolu tekir kedisi. Evde rahat bir ortamı sever.",
                    PhotoPath = "cat3.jpg", // Use the filename you added
                    OwnerId = defaultUser.Id,
                    PhoneNumber = "5551234569",
                    CreatedDate = DateTime.Now
                }
            };

            // Create 3 dogs
            var dogs = new[]
            {
                new Animal
                {
                    Name = "Karabaş",
                    Type = "Köpek",
                    Breed = "Golden Retriever",
                    Age = 2,
                    Gender = "Erkek",
                    IsVaccinated = true,
                    Description = "Sadık ve sevgi dolu Golden Retriever. Çocuklarla çok iyi geçinir.",
                    PhotoPath = "dog1.jpg", // Use the filename you added
                    OwnerId = defaultUser.Id,
                    PhoneNumber = "5551234570",
                    CreatedDate = DateTime.Now
                },
                new Animal
                {
                    Name = "Zeytin",
                    Type = "Köpek",
                    Breed = "Kangal",
                    Age = 4,
                    Gender = "Dişi",
                    IsVaccinated = true,
                    Description = "Koruyucu ve sadık Kangal köpeği. Aileye çok bağlıdır.",
                    PhotoPath = "dog2.jpg", // Use the filename you added
                    OwnerId = defaultUser.Id,
                    PhoneNumber = "5551234571",
                    CreatedDate = DateTime.Now
                },
                new Animal
                {
                    Name = "Miskin",
                    Type = "Köpek",
                    Breed = "Karisik",
                    Age = 1,
                    Gender = "Erkek",
                    IsVaccinated = true,
                    Description = "Oyuncak ve enerjik karisik irk kopek. Hareketli bir ortamda daha mutludur.",
                    PhotoPath = "dog3.jpg", // Use the filename you added
                    OwnerId = defaultUser.Id,
                    PhoneNumber = "5551234572",
                    CreatedDate = DateTime.Now
                }
            };

            // Add all animals to the context
            await _context.Animals.AddRangeAsync(cats);
            await _context.Animals.AddRangeAsync(dogs);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok("Successfully added 6 animals (3 cats and 3 dogs) to the database.");
        }

        [HttpPost("seed-donation")]
        public async Task<IActionResult> SeedDonation()
        {
            // Get the first user from the database
            var user = await _context.Users.FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı. Lütfen önce bir kullanıcı oluşturun.");
            }

            // Get the first animal from the database
            var animal = await _context.Animals.FirstOrDefaultAsync();
            if (animal == null)
            {
                return BadRequest("Hayvan bulunamadı. Lütfen önce bir hayvan ekleyin.");
            }

            decimal testAmount = 250.00m;
            // Check if a test donation already exists
            var existingDonation = await _context.Donations
                .Where(d => d.UserId == user.Id && d.AnimalId == animal.Id && d.Amount == testAmount)
                .FirstOrDefaultAsync();
                
            if (existingDonation != null)
            {
                return BadRequest("Test bağışı zaten mevcut.");
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

            return Ok($"Test bağışı başarıyla eklendi: {donation.DonorName}, {donation.Amount} TL");
        }

        [HttpPost("seed-users")]
        public async Task<IActionResult> SeedUsers()
        {
            // Check if users already exist
            var existingUsers = await _context.Users.AnyAsync();
            if (existingUsers)
            {
                return BadRequest("Kullanıcılar zaten mevcut.");
            }

            // Create adopter user (Sahiplenici)
            var adopterUser = new ApplicationUser
            {
                UserName = "sahiplenici@test.com",
                Email = "sahiplenici@test.com",
                FullName = "Sahiplenici Kullanıcı",
                EmailConfirmed = true
            };

            var adopterResult = await _userManager.CreateAsync(adopterUser, "Sifre123!");
            if (!adopterResult.Succeeded)
            {
                return BadRequest($"Sahiplenici kullanıcısı oluşturulamadı: {string.Join(", ", adopterResult.Errors.Select(e => e.Description))}");
            }

            // Add adopter user to role
            await _userManager.AddToRoleAsync(adopterUser, "Sahiplenici");

            // Create owner user (Sahip)
            var ownerUser = new ApplicationUser
            {
                UserName = "sahip@test.com",
                Email = "sahip@test.com",
                FullName = "Sahip Kullanıcı",
                EmailConfirmed = true
            };

            var ownerResult = await _userManager.CreateAsync(ownerUser, "Sifre123!");
            if (!ownerResult.Succeeded)
            {
                return BadRequest($"Sahip kullanıcısı oluşturulamadı: {string.Join(", ", ownerResult.Errors.Select(e => e.Description))}");
            }

            // Add owner user to role
            await _userManager.AddToRoleAsync(ownerUser, "Sahip");

            return Ok("İki kullanıcı başarıyla oluşturuldu: Sahiplenici ve Sahip");
        }

        [HttpPost("seed-more-animals")]
        public async Task<IActionResult> SeedMoreAnimals()
        {
            // Create a default user if one doesn't exist
            var defaultUser = await _context.Users.FirstOrDefaultAsync();
            if (defaultUser == null)
            {
                return BadRequest("No user found. Please register a user first.");
            }

            // Check if the specific animals we want to add already exist
            var existingAnimalNames = new[] { "Prenses", "Baron", "Prens" };
            var existingAnimals = await _context.Animals
                .Where(a => existingAnimalNames.Contains(a.Name))
                .Select(a => a.Name)
                .ToListAsync();

            if (existingAnimals.Count >= 3) // All 3 animals already exist
            {
                return BadRequest("The additional animals already exist in the database.");
            }

            // Create animals that don't already exist
            
            if (!existingAnimals.Contains("Prenses"))
            {
                var prenses = new Animal
                {
                    Name = "Prenses",
                    Type = "Kedi",
                    Breed = "British Shorthair",
                    Age = 2,
                    Gender = "Dişi",
                    IsVaccinated = true,
                    Description = "Sakin ve sevimli British Shorthair kedisi. Temiz ve düzenli bir yaşam tarzı sever.",
                    PhotoPath = "cat4.jpg",
                    OwnerId = defaultUser.Id,
                    PhoneNumber = "5551234573",
                    CreatedDate = DateTime.Now
                };
                _context.Animals.Add(prenses);
            }
            
            if (!existingAnimals.Contains("Baron"))
            {
                var baron = new Animal
                {
                    Name = "Baron",
                    Type = "Köpek",
                    Breed = "Labrador Retriever",
                    Age = 3,
                    Gender = "Erkek",
                    IsVaccinated = true,
                    Description = "Dost canlısı ve sadık Labrador. Özellikle çocuklarla iyi geçinir.",
                    PhotoPath = "dog4.jpg",
                    OwnerId = defaultUser.Id,
                    PhoneNumber = "5551234574",
                    CreatedDate = DateTime.Now
                };
                _context.Animals.Add(baron);
            }
            
            if (!existingAnimals.Contains("Prens"))
            {
                var prens = new Animal
                {
                    Name = "Prens",
                    Type = "Köpek",
                    Breed = "Bulldog",
                    Age = 4,
                    Gender = "Erkek",
                    IsVaccinated = true,
                    Description = "Sakin ve koruyucu Bulldog. Ailesine çok bağlıdır.",
                    PhotoPath = "dog5.jpg",
                    OwnerId = defaultUser.Id,
                    PhoneNumber = "5551234575",
                    CreatedDate = DateTime.Now
                };
                _context.Animals.Add(prens);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok($"Successfully added new animals to the database.");
        }

        [HttpPost("update-animal-phones")]
        public async Task<IActionResult> UpdateAnimalPhones()
        {
            // Get all animals without phone numbers
            var animalsWithoutPhones = await _context.Animals
                .Where(a => string.IsNullOrEmpty(a.PhoneNumber))
                .ToListAsync();

            if (animalsWithoutPhones.Count == 0)
            {
                return Ok("All animals already have phone numbers.");
            }

            // Assign phone numbers to animals without them
            string[] phoneNumbers = { "5551234567", "5551234568", "5551234569", "5551234570", "5551234571", "5551234572", "5551234573", "5551234574", "5551234575" };
            
            for (int i = 0; i < animalsWithoutPhones.Count && i < phoneNumbers.Length; i++)
            {
                animalsWithoutPhones[i].PhoneNumber = phoneNumbers[i];
            }

            await _context.SaveChangesAsync();

            return Ok($"Successfully updated {animalsWithoutPhones.Count} animals with phone numbers.");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatiYuva.Data;
using PatiYuva.Models;
using System;
using System.Threading.Tasks;

namespace PatiYuva.Controllers
{
    public class TempController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TempController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Seed()
        {
            // Check if animals already exist
            var existingAnimals = await _context.Animals.AnyAsync();
            if (existingAnimals)
            {
                return Content("Animals already exist in the database.");
            }

            // Get a default user
            var defaultUser = await _context.Users.FirstOrDefaultAsync();
            if (defaultUser == null)
            {
                return Content("No user found. Please register a user first.");
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
                    PhotoPath = "cat1.jpg",
                    OwnerId = defaultUser.Id,
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
                    PhotoPath = "cat2.jpg",
                    OwnerId = defaultUser.Id,
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
                    PhotoPath = "cat3.jpg",
                    OwnerId = defaultUser.Id,
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
                    PhotoPath = "dog1.jpg",
                    OwnerId = defaultUser.Id,
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
                    PhotoPath = "dog2.jpg",
                    OwnerId = defaultUser.Id,
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
                    PhotoPath = "dog3.jpg",
                    OwnerId = defaultUser.Id,
                    CreatedDate = DateTime.Now
                }
            };

            // Add all animals to the context
            await _context.Animals.AddRangeAsync(cats);
            await _context.Animals.AddRangeAsync(dogs);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Content("Successfully added 6 animals (3 cats and 3 dogs) to the database.");
        }
    }
}
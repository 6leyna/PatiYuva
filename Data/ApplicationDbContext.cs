using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatiYuva.Models;

namespace PatiYuva.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Animal entity
            builder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Breed).HasMaxLength(100);
                entity.Property(e => e.Gender).HasMaxLength(20);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.PhotoPath).HasMaxLength(500);
                
                // Configure relationship with Owner
                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.OwnedAnimals)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // Configure AdoptionRequest entity
            builder.Entity<AdoptionRequest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.Message).HasMaxLength(500);
                
                // Configure relationship with Animal
                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.AdoptionRequests)
                    .HasForeignKey(d => d.AnimalId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                
                // Configure relationship with Requester
                entity.HasOne(d => d.Requester)
                    .WithMany()
                    .HasForeignKey(d => d.RequesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // Configure Donation entity
            builder.Entity<Donation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DonorName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DonorEmail).IsRequired().HasMaxLength(256);
                entity.Property(e => e.BankName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Iban).IsRequired().HasMaxLength(34);
                entity.Property(e => e.Message).HasMaxLength(500);
                
                // Configure relationship with User
                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                
                // Configure relationship with Animal
                entity.HasOne(d => d.Animal)
                    .WithMany()
                    .HasForeignKey(d => d.AnimalId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // Seed roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Sahip", NormalizedName = "SAHIP" },
                new IdentityRole { Name = "Sahiplenici", NormalizedName = "SAHIPLENCILI" }
            );
        }
    }
}
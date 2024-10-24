using BetCatalog.Models.Persons;
using BetCatalog.Models.Accounts;
using BetCatalog.Models.BankAccounts;
using BetCatalog.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BetCatalog.Infrastructure.EFCore
{
    public class BetCatalogDbContext(DbContextOptions<BetCatalogDbContext> options)
        : IdentityDbContext<User>(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Person> Persons { get; set; }
        

        public DbSet<UserSetting> UserSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasData(new UserRole { Id = "2e7c776d-803d-4483-83e4-4727c0db0142", Name = UserRole.Admin, NormalizedName = UserRole.Admin.ToUpper() });

            modelBuilder.Entity<Person>()
                .HasMany<Account>()
                .WithOne(c => c.Person)
                .HasForeignKey(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .HasMany<BankAccount>()
                .WithOne(c => c.Person)
                .HasForeignKey(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

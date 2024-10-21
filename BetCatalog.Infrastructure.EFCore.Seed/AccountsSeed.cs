using BetCatalog.Models.Accounts;
using BetCatalog.Models.Persons;

namespace BetCatalog.Infrastructure.EFCore.Seed
{
    internal class AccountsSeed : ISeed
    {
        public void Run(BetCatalogDbContext dbContext)
        {
            var person = new Person 
            {
                FIO = "DDD",
                TelegramId = "qqq",
                PhoneNumber = "1234",
                Bank = "dada",
                BirthDate = "2022-22-11",
                PassportDate = "2022-01-01"
            };

            var account = new Account
            {
                Login = "add",
                Password = "1234"
            };
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();

        }
    }
}

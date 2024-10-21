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
                FIO = "Иван Иваныч Иваненко",
                TelegramId = "qqq",
                PhoneNumber = "41414141",
                Bank = "41414141",
                BirthDate = new DateOnly(2002, 1, 1),
                PassportDate = new DateOnly(2022, 1, 1)
            };

            var account = new Account
            {
                Status = 1,
                Login = "DADDA13131",
                Password = "12345",
                Person = person
            };

            var person1 = new Person 
            {
                FIO = "Зуберко Михаил Петрович",
                TelegramId = "bxbx",
                PhoneNumber = "51551",
                Bank = "86818618",
                BirthDate = new DateOnly(2002, 1, 1),
                PassportDate = new DateOnly(2022, 1, 1)
            };

            var account1 = new Account
            {
                Status = 1,
                Login = "GGAGA22",
                Password = "12345",
                Person = person1
            };
            dbContext.Accounts.Add(account);
            dbContext.Accounts.Add(account1);
            dbContext.SaveChanges();

        }
    }
}

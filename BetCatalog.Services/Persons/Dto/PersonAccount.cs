using BetCatalog.Models.Accounts;

namespace BetCatalog.Services.Persons.Dto
{
    public class PersonAccount
    {
        public int Id { get; set; }
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
        public AccountStatus Status { get; set; }
    }
}

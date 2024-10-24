using BetCatalog.Models.Persons;

namespace BetCatalog.Models.Accounts
{
    public class Account : IEntity
    {
        public int Id { get; set; }
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
        public AccountStatus Status { get; set; }

        public int? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}

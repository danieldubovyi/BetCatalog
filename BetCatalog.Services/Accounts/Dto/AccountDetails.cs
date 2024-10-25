using BetCatalog.Models.Accounts;

namespace BetCatalog.Services.Accounts.Dto
{
    public class AccountDetails
    {
        public int Id { get; set; }
        public AccountStatus Status { get; set; }
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;

        public int? PersonId { get; set; }
    }
}

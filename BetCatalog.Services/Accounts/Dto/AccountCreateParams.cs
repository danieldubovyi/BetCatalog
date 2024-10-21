using BetCatalog.Models.Accounts;

namespace BetCatalog.Services.Accounts.Dto
{
    public class AccountCreateParams
    {
        public AccountStatus Status { get; set; }
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;

        public int PersonId { get; set; }
    }
}

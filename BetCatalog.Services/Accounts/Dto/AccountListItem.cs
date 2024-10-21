using BetCatalog.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetCatalog.Services.Accounts.Dto
{
    public class AccountListItem
    {
        public int Id { get; set; }
        public AccountStatus Status { get; set; }
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;

    }
}

using BetCatalog.Models.Accounts;
using System.Linq.Expressions;

namespace BetCatalog.Services.Accounts.Dto
{
    public class AccountFilter
    {
        public AccountStatus? Status { get; set; }
        public int? PersonId { get; set; }

        public Expression<Func<Account, bool>>? GetExpression()
        {
            List<Expression<Func<Account, bool>>> expressions = [];

            if (Status.HasValue)
            {
                expressions.Add(a => a.Status == Status.Value);
            }

            if (PersonId.HasValue)
            {
                expressions.Add(a => a.PersonId == PersonId.Value);
            }

            return expressions.ToAndExpression();
        }
    }
}

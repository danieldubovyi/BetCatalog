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

            // if (IsOrdered.HasValue)
            // {
            //     expressions.Add(c => c.IsOrdered);   
            // }

            // if (IsDelivered.HasValue)
            // {
            //     expressions.Add(c => c.IsDelivered);
            // }

            if (PersonId.HasValue)
            {
                expressions.Add(c => c.PersonId == PersonId.Value);
            }

            return expressions.ToAndExpression();
        }
    }
}

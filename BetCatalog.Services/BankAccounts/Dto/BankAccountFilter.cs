using BetCatalog.Models.BankAccounts;
using System.Linq.Expressions;

namespace BetCatalog.Services.BankAccounts.Dto
{
    public class BankAccountFilter
    {
        public PaymentType? PaymentType { get; set; }
        public int? PersonId { get; set; }

        public Expression<Func<BankAccount, bool>>? GetExpression()
        {
            List<Expression<Func<BankAccount, bool>>> expressions = [];
            
            if (PaymentType.HasValue)
            {
                expressions.Add(a => a.PaymentType == PaymentType.Value);
            }

            if (PersonId.HasValue)
            {
                expressions.Add(c => c.PersonId == PersonId.Value);
            }

            return expressions.ToAndExpression();
        }
    }
}

using System.Linq.Expressions;
using System.Numerics;
using BetCatalog.Models.BankAccounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.BankAccounts.Dto;
using MediatR;

namespace BetCatalog.Services.BankAccounts.Queries
{
    public record GetBankAccountsQuery(BankAccountFilter Filter)
        : IRequest<IReadOnlyCollection<BankAccountListItem>>;

    internal class GetBankAccountsQueryHandler(IEntityRepository<BankAccount> bankAccountRepository)
        : IRequestHandler<GetBankAccountsQuery, IReadOnlyCollection<BankAccountListItem>>
    {
        public async Task<IReadOnlyCollection<BankAccountListItem>> Handle(
            GetBankAccountsQuery request,
            CancellationToken cancellationToken
        )
        {
            return await bankAccountRepository.GetProjectedListAsync(
                request.Filter.GetExpression(),
                c => new BankAccountListItem
                {
                    Id = c.Id,
                    BankName = c.BankName,
                    PaymentType = c.PaymentType,
                    CardNumber = c.CardNumber,
                    CVV = c.CVV,
                    ValidationEndDate = c.ValidationEndDate,
                },
                cancellationToken
            );
        }

        //private static Expression<Func<BankAccount, bool>>? GetFilter(GetBankAccountsQuery query)
        //{
        //    List<Expression<Func<BankAccount, bool>>> expressions = [];

        //    if (query.NotInTeam.HasValue)
        //    {
        //        if (query.NotInTeam.Value)
        //        {
        //            expressions.Add(p => p.TeamId == null);
        //        }
        //        else
        //        {
        //            expressions.Add(p => p.TeamId != null);
        //        }
        //    }

        //    return expressions.ToAndExpression();
        //}
    }
}

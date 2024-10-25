using BetCatalog.Models.Accounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Accounts.Dto;
using MediatR;

namespace BetCatalog.Services.Accounts.Queries
{
    public record GetAccountsQuery(AccountFilter Filter)
        : IRequest<IReadOnlyCollection<AccountListItem>>;

    internal class GetAccountsQueryHandler(IEntityRepository<Account> accountRepository)
        : IRequestHandler<GetAccountsQuery, IReadOnlyCollection<AccountListItem>>
    {
        public async Task<IReadOnlyCollection<AccountListItem>> Handle(
            GetAccountsQuery request,
            CancellationToken cancellationToken
        )
        {
            return await accountRepository.GetProjectedListAsync(
                request.Filter.GetExpression(),
                c => new AccountListItem
                {
                    Id = c.Id,
                    Login = c.Login,
                    Password = c.Password,
                    Status = c.Status,
                },
                cancellationToken
            );
        }

        //private static Expression<Func<Account, bool>>? GetFilter(GetAccountsQuery query)
        //{
        //    List<Expression<Func<Account, bool>>> expressions = [];

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

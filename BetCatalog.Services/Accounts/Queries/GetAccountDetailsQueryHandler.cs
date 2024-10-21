using BetCatalog.Models.Accounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Accounts.Dto;
using MediatR;

namespace BetCatalog.Services.Accounts.Queries
{
    public record GetAccountDetailsQuery(int AccountId) : IRequest<AccountDetails>;
    internal class GetAccountDetailsQueryHandler(IEntityRepository<Account> accountRepository)
        : IRequestHandler<GetAccountDetailsQuery, AccountDetails>
    {
        public async Task<AccountDetails> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
        {
            return await accountRepository.GetAsync(request.AccountId,
                c => new AccountDetails
                {
                    Id = c.Id,
                    PersonId = c.PersonId,
                    Login = c.Login,
                    Password = c.Password,
                    Status = c.Status
                }, cancellationToken);
        }
    }
}

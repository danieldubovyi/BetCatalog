using BetCatalog.Models.Accounts;
using BetCatalog.Models.Interfaces.Repositories;
using MediatR;

namespace BetCatalog.Services.Persons.Commands
{
    public record UpdatePersonAccountsCommand(int PersonId, IReadOnlyCollection<int> AccountsIds) : IRequest;
    internal class UpdatePersonAccountsCommandHandler(IEntityRepository<Account> accountRepository)
        : IRequestHandler<UpdatePersonAccountsCommand>
    {
        public async Task Handle(UpdatePersonAccountsCommand request, CancellationToken cancellationToken)
        {
            var accounts = await accountRepository.GetListAsync(c => c.PersonId == request.PersonId || request.AccountsIds.Contains(c.Id), cancellationToken);

            foreach (var account in accounts)
            {
                account.PersonId = request.AccountsIds.Contains(account.Id) ? request.PersonId : null;
            }

            await accountRepository.UpdateAsync(accounts, cancellationToken);
        }
    }
}

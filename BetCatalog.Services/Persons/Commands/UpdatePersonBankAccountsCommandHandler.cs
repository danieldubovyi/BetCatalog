using BetCatalog.Models.BankAccounts;
using BetCatalog.Models.Interfaces.Repositories;
using MediatR;

namespace BetCatalog.Services.Persons.Commands
{
    public record UpdatePersonBankAccountsCommand(int PersonId, IReadOnlyCollection<int> BankAccountsIds) : IRequest;
    internal class UpdatePersonBankAccountsCommandHandler(IEntityRepository<BankAccount> bankAccountRepository)
        : IRequestHandler<UpdatePersonBankAccountsCommand>
    {
        public async Task Handle(UpdatePersonBankAccountsCommand request, CancellationToken cancellationToken)
        {
            var bankAccounts = await bankAccountRepository.GetListAsync(c => c.PersonId == request.PersonId || request.BankAccountsIds.Contains(c.Id), cancellationToken);

            foreach (var bankAccount in bankAccounts)
            {
                bankAccount.PersonId = request.BankAccountsIds.Contains(bankAccount.Id) ? request.PersonId : null;
            }

            await bankAccountRepository.UpdateAsync(bankAccounts, cancellationToken);
        }
    }
}

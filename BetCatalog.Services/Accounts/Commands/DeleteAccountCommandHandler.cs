using BetCatalog.Models.Accounts;
using BetCatalog.Models.Interfaces.Repositories;
using MediatR;

namespace BetCatalog.Services.Accounts.Commands
{
    public record DeleteAccountCommand(int AccountId) : IRequest;
    internal class DeleteAccountCommandHandler(IEntityRepository<Account> accountRepository)
        : IRequestHandler<DeleteAccountCommand>
    {
        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            await accountRepository.DeleteAsync(request.AccountId, cancellationToken);
        }
    }
}

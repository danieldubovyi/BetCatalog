using BetCatalog.Models.BankAccounts;
using BetCatalog.Models.Interfaces.Repositories;
using MediatR;

namespace BetCatalog.Services.BankAccounts.Commands
{
    public record DeleteBankAccountCommand(int BankAccountId) : IRequest;
    internal class DeleteBankAccountCommandHandler(IEntityRepository<BankAccount> bankAccountRepository)
        : IRequestHandler<DeleteBankAccountCommand>
    {
        public async Task Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
        {
            await bankAccountRepository.DeleteAsync(request.BankAccountId, cancellationToken);
        }
    }
}

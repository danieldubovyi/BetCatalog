using BetCatalog.Models.Accounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Accounts.Dto;
using MediatR;

namespace BetCatalog.Services.Accounts.Commands
{
    public record UpdateAccountCommand(int AccountId, AccountCreateParams CreateParams) : IRequest;
    internal class UpdateAccountCommandHandler(IEntityRepository<Account> accountRepository)
        : IRequestHandler<UpdateAccountCommand>
    {
        public async Task Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await accountRepository.GetAsync(request.AccountId, cancellationToken);
            account.Login = request.CreateParams.Login;
            account.Password = request.CreateParams.Password;
            account.Status = request.CreateParams.Status;
            account.PersonId = request.CreateParams.PersonId;

            await accountRepository.UpdateAsync(account, cancellationToken);
        }
    }
}

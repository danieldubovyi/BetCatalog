using BetCatalog.Models.Accounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Accounts.Dto;
using MediatR;

namespace BetCatalog.Services.Accounts.Commands
{
    public record CreateAccountCommand(AccountCreateParams CreateParams) : IRequest<int>;
    internal class CreateAccountCommandHandler(IEntityRepository<Account> accountRepository)
        : IRequestHandler<CreateAccountCommand, int>
    {
        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                PersonId = request.CreateParams.PersonId,
                Login = request.CreateParams.Login,
                Password = request.CreateParams.Password,
                Status = request.CreateParams.Status,
            };
            await accountRepository.CreateAsync(account, cancellationToken);
            return account.Id;
        }
    }
}

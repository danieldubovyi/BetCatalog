using BetCatalog.Models.BankAccounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.BankAccounts.Dto;
using MediatR;

namespace BetCatalog.Services.BankAccounts.Commands
{
    public record CreateBankAccountCommand(BankAccountCreateParams CreateParams) : IRequest<int>;

    internal class CreateBankAccountCommandHandler(
        IEntityRepository<BankAccount> bankAccountRepository
    ) : IRequestHandler<CreateBankAccountCommand, int>
    {
        public async Task<int> Handle(
            CreateBankAccountCommand request,
            CancellationToken cancellationToken
        )
        {
            var bankAccount = new BankAccount
            {
                BankName = request.CreateParams.BankName,
                PinCode = request.CreateParams.PinCode,
                INN = request.CreateParams.INN,
                CardNumber = request.CreateParams.CardNumber,
                CVV = request.CreateParams.CVV,
                PaymentType = request.CreateParams.PaymentType,
                ValidationEndDate = request.CreateParams.ValidationEndDate,

                PersonId = request.CreateParams.PersonId,
            };
            await bankAccountRepository.CreateAsync(bankAccount, cancellationToken);
            return bankAccount.Id;
        }
    }
}

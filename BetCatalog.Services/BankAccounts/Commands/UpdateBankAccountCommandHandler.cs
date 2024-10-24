using BetCatalog.Models.BankAccounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.BankAccounts.Dto;
using MediatR;

namespace BetCatalog.Services.BankAccounts.Commands
{
    public record UpdateBankAccountCommand(int BankAccountId, BankAccountCreateParams CreateParams)
        : IRequest;

    internal class UpdateBankAccountCommandHandler(
        IEntityRepository<BankAccount> bankAccountRepository
    ) : IRequestHandler<UpdateBankAccountCommand>
    {
        public async Task Handle(
            UpdateBankAccountCommand request,
            CancellationToken cancellationToken
        )
        {
            var bankAccount = await bankAccountRepository.GetAsync(
                request.BankAccountId,
                cancellationToken
            );
            bankAccount.BankName = request.CreateParams.BankName;
            bankAccount.PinCode = request.CreateParams.PinCode;
            bankAccount.INN = request.CreateParams.INN;
            bankAccount.CardNumber = request.CreateParams.CardNumber;
            bankAccount.CVV = request.CreateParams.CVV;
            bankAccount.PaymentType = request.CreateParams.PaymentType;
            bankAccount.ValidationEndDate = request.CreateParams.ValidationEndDate;
            bankAccount.PersonId = request.CreateParams.PersonId;

            await bankAccountRepository.UpdateAsync(bankAccount, cancellationToken);
        }
    }
}

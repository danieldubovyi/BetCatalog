using BetCatalog.Models.BankAccounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.BankAccounts.Dto;
using MediatR;

namespace BetCatalog.Services.BankAccounts.Queries
{
    public record GetBankAccountDetailsQuery(int BankAccountId) : IRequest<BankAccountDetails>;

    internal class GetBankAccountDetailsQueryHandler(
        IEntityRepository<BankAccount> bankAccountRepository
    ) : IRequestHandler<GetBankAccountDetailsQuery, BankAccountDetails>
    {
        public async Task<BankAccountDetails> Handle(
            GetBankAccountDetailsQuery request,
            CancellationToken cancellationToken
        )
        {
            return await bankAccountRepository.GetAsync(
                request.BankAccountId,
                c => new BankAccountDetails
                {
                    Id = c.Id,
                    BankName = c.BankName,
                    PinCode = c.PinCode,
                    INN = c.INN,
                    CardNumber = c.CardNumber,
                    CVV = c.CVV,
                    PaymentType = c.PaymentType,
                    ValidationEndDate = c.ValidationEndDate,
                    PersonId = c.PersonId,
                },
                cancellationToken
            );
        }
    }
}

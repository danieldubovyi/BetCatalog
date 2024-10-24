using BetCatalog.Models.BankAccounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Persons.Dto;
using MediatR;

namespace BetCatalog.Services.Persons.Queries
{
    public record GetPersonBankAccountsQuery(int PersonId)
        : IRequest<IReadOnlyCollection<PersonBankAccount>>;

    internal class GetPersonBankAccountsQueryHandler(
        IEntityRepository<BankAccount> bankAccountRepository
    ) : IRequestHandler<GetPersonBankAccountsQuery, IReadOnlyCollection<PersonBankAccount>>
    {
        public async Task<IReadOnlyCollection<PersonBankAccount>> Handle(
            GetPersonBankAccountsQuery request,
            CancellationToken cancellationToken
        )
        {
            return await bankAccountRepository.GetProjectedListAsync(
                c => c.PersonId == request.PersonId,
                c => new PersonBankAccount
                {
                    Id = c.Id,
                    BankName = c.BankName,
                    PinCode = c.PinCode,
                    INN = c.INN,
                    CardNumber = c.CardNumber,
                    CVV = c.CVV,
                    PaymentType = c.PaymentType,
                    ValidationEndDate = c.ValidationEndDate,
                },
                cancellationToken
            );
        }
    }
}

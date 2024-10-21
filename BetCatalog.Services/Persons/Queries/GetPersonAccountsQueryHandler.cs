using BetCatalog.Models.Accounts;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Persons.Dto;
using MediatR;

namespace BetCatalog.Services.Persons.Queries
{
    public record GetPersonAccountsQuery(int PersonId) : IRequest<IReadOnlyCollection<PersonAccount>>;
    internal class GetPersonAccountsQueryHandler(IEntityRepository<Account> accountRepository)
        : IRequestHandler<GetPersonAccountsQuery, IReadOnlyCollection<PersonAccount>>
    {
        public async Task<IReadOnlyCollection<PersonAccount>> Handle(GetPersonAccountsQuery request, CancellationToken cancellationToken)
        {
            return await accountRepository.GetProjectedListAsync(
                c => c.PersonId == request.PersonId,
                c => new PersonAccount
                {
                    Id = c.Id,
                    Login = c.Login,
                    Password = c.Password,
                    Status = c.Status
                },cancellationToken);
        }
    }
}

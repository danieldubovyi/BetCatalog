using BetCatalog.Models.Persons;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Persons.Dto;
using MediatR;

namespace BetCatalog.Services.Persons.Queries
{
    public record GetPersonsQuery : IRequest<IReadOnlyCollection<PersonListItem>>;
    internal class GetPersonsQueryHandler(IEntityRepository<Person> personRepository)
        : IRequestHandler<GetPersonsQuery, IReadOnlyCollection<PersonListItem>>
    {
        public async Task<IReadOnlyCollection<PersonListItem>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
        {
            return await personRepository.GetProjectedListAsync(
                p => new PersonListItem
                {
                    Id = p.Id,
                    FIO = p.FIO,
                    TelegramId = p.TelegramId,
                    PersonType = p.PersonType
                }, cancellationToken);
        }
    }
}

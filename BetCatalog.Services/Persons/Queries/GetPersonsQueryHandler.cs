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
                d => new PersonListItem
                {
                    Id = d.Id,
                    FIO = d.FIO,
                    TelegramId = d.TelegramId
                }, cancellationToken);
        }
    }
}

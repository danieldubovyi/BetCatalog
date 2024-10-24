using BetCatalog.Models.Persons;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Persons.Dto;
using MediatR;

namespace BetCatalog.Services.Persons.Queries
{
    public record GetPersonDetailsQuery(int PersonId) : IRequest<PersonDetails>;
    internal class GetPersonDetailsQueryHandler(IEntityRepository<Person> personRepository)
        : IRequestHandler<GetPersonDetailsQuery, PersonDetails>
    {
        public async Task<PersonDetails> Handle(GetPersonDetailsQuery request, CancellationToken cancellationToken)
        {
            return await personRepository.GetAsync(request.PersonId,
                p => new PersonDetails
                {
                    Id = p.Id,
                    FIO = p.FIO,
                    TelegramId = p.TelegramId,
                    PhoneNumber = p.PhoneNumber,
                    BirthDate = p.BirthDate,
                    PassportDate = p.PassportDate,
                    PersonType = p.PersonType,
                }, cancellationToken);
        }
    }
}

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
                d => new PersonDetails
                {
                    Id = d.Id,
                    FIO = d.FIO,
                    TelegramId = d.TelegramId,
                    PhoneNumber = d.PhoneNumber,
                    Bank = d.Bank,
                    BirthDate = d.BirthDate,
                    PassportDate = d.PassportDate
                }, cancellationToken);
        }
    }
}

using BetCatalog.Models.Persons;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Persons.Dto;
using MediatR;

namespace BetCatalog.Services.Persons.Commands
{
    public record CreatePersonCommand(PersonCreateParams CreateParams) : IRequest<int>;
    internal class CreatePersonCommandHandler(IEntityRepository<Person> personRepository)
        : IRequestHandler<CreatePersonCommand, int>
    {
        public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                FIO = request.CreateParams.FIO,
                TelegramId = request.CreateParams.TelegramId,
                PhoneNumber = request.CreateParams.PhoneNumber,
                BirthDate = request.CreateParams.BirthDate,
                PassportDate = request.CreateParams.PassportDate,
                PersonType = request.CreateParams.PersonType,

            };
            await personRepository.CreateAsync(person, cancellationToken);
            return person.Id;
        }
    }
}

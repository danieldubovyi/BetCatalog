using BetCatalog.Models.Persons;
using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Services.Persons.Dto;
using MediatR;

namespace BetCatalog.Services.Persons.Commands
{
    public record UpdatePersonCommand(int PersonId, PersonCreateParams CreateParams) : IRequest;
    internal class UpdatePersonCommandHandler(IEntityRepository<Person> personRepository)
        : IRequestHandler<UpdatePersonCommand>
    {
        public async Task Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await personRepository.GetAsync(request.PersonId, cancellationToken);
            person.FIO = request.CreateParams.FIO;
            person.TelegramId = request.CreateParams.TelegramId;
            person.PhoneNumber = request.CreateParams.PhoneNumber;
            person.Bank = request.CreateParams.Bank;
            person.BirthDate = request.CreateParams.BirthDate;
            person.PassportDate = request.CreateParams.PassportDate;
            await personRepository.UpdateAsync(person, cancellationToken);
        }
    }
}

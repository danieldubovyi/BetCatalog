using BetCatalog.Models.Persons;
using BetCatalog.Models.Interfaces.Repositories;
using MediatR;

namespace BetCatalog.Services.Persons.Commands
{
    public record DeletePersonCommand(int PersonId) : IRequest;
    internal class DeletePersonCommandHandler(IEntityRepository<Person> personRepository)
        : IRequestHandler<DeletePersonCommand>
    {
        public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            await personRepository.DeleteAsync(request.PersonId, cancellationToken);
        }
    }
}

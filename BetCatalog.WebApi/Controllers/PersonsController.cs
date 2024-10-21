using BetCatalog.Models.Users;
using BetCatalog.Services.Accounts.Dto;
using BetCatalog.Services.Accounts.Queries;
using BetCatalog.Services.Persons.Commands;
using BetCatalog.Services.Persons.Dto;
using BetCatalog.Services.Persons.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetCatalog.WebApi.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController(ISender sender)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyCollection<PersonListItem>> GetPersons(CancellationToken cancellationToken)
        {
            var query = new GetPersonsQuery();
            return await sender.Send(query, cancellationToken);
        }
        [HttpGet("{personId:int}")]
        public async Task<PersonDetails> GetPerson(int PersonId, CancellationToken cancellationToken)
        {
            var query = new GetPersonDetailsQuery(PersonId);
            return await sender.Send(query, cancellationToken);
        }

        [HttpGet("{personId:int}/accounts")]
        public async Task<IReadOnlyCollection<PersonAccount>> GetPersonAccounts(int PersonId, CancellationToken cancellation)
        {
            var query = new GetPersonAccountsQuery(PersonId);
            return await sender.Send(query, cancellation);
        }

        [HttpPost]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task<int> CreatePerson(PersonCreateParams PersonCreateParams, CancellationToken cancellationToken)
        {
            return await sender.Send(new CreatePersonCommand(PersonCreateParams), cancellationToken);
        }

        [HttpPut]
        [Authorize(Roles = UserRole.Admin)]
        public async Task UpdatePerson(int PersonId, PersonCreateParams PersonCreateParams, CancellationToken cancellationToken)
        {
            await sender.Send(new UpdatePersonCommand(PersonId, PersonCreateParams), cancellationToken);
        }

        [HttpDelete("{personId:int}")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task DeletePerson(int PersonId, CancellationToken cancellationToken)
        {
            await sender.Send(new DeletePersonCommand(PersonId), cancellationToken);
        }

        [HttpPut("{personId:int}/accounts")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task UpdatePersonAccounts(int PersonId, IReadOnlyCollection<int> componenetsIds, CancellationToken cancellationToken)
        {
            await sender.Send(new UpdatePersonAccountsCommand(PersonId, componenetsIds), cancellationToken);
        }
    }
}

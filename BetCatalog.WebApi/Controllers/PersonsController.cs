using BetCatalog.Models.Users;
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
    public class PersonsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyCollection<PersonListItem>> GetPersons(
            CancellationToken cancellationToken
        )
        {
            var query = new GetPersonsQuery();
            return await sender.Send(query, cancellationToken);
        }

        [HttpGet("{personId:int}")]
        public async Task<PersonDetails> GetPerson(
            int personId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetPersonDetailsQuery(personId);
            return await sender.Send(query, cancellationToken);
        }

        [HttpGet("{personId:int}/accounts")]
        public async Task<IReadOnlyCollection<PersonAccount>> GetPersonAccounts(
            int personId,
            CancellationToken cancellation
        )
        {
            var query = new GetPersonAccountsQuery(personId);
            return await sender.Send(query, cancellation);
        }

        [HttpGet("{personId:int}/bankAccounts")]
        public async Task<IReadOnlyCollection<PersonBankAccount>> GetPersonBankAccounts(
            int personId,
            CancellationToken cancellation
        )
        {
            var query = new GetPersonBankAccountsQuery(personId);
            return await sender.Send(query, cancellation);
        }

        [HttpPost]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task<int> CreatePerson(
            PersonCreateParams personCreateParams,
            CancellationToken cancellationToken
        )
        {
            return await sender.Send(
                new CreatePersonCommand(personCreateParams),
                cancellationToken
            );
        }

        [HttpPut("{personId:int}")]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task UpdatePerson(
            int personId,
            PersonCreateParams personCreateParams,
            CancellationToken cancellationToken
        )
        {
            await sender.Send(
                new UpdatePersonCommand(personId, personCreateParams),
                cancellationToken
            );
        }

        [HttpDelete("{personId:int}")]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task DeletePerson(int personId, CancellationToken cancellationToken)
        {
            await sender.Send(new DeletePersonCommand(personId), cancellationToken);
        }

        [HttpPut("{personId:int}/accounts")]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task UpdatePersonAccounts(
            int personId,
            IReadOnlyCollection<int> accountsIds,
            CancellationToken cancellationToken
        )
        {
            await sender.Send(
                new UpdatePersonAccountsCommand(personId, accountsIds),
                cancellationToken
            );
        }

        [HttpPut("{personId:int}/bankAccounts")]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task UpdatePersonBankAccounts(
            int personId,
            IReadOnlyCollection<int> bankAccountsIds,
            CancellationToken cancellationToken
        )
        {
            await sender.Send(
                new UpdatePersonBankAccountsCommand(personId, bankAccountsIds),
                cancellationToken
            );
        }
    }
}

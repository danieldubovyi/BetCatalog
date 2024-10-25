using BetCatalog.Models.Users;
using BetCatalog.Services.Accounts.Commands;
using BetCatalog.Services.Accounts.Dto;
using BetCatalog.Services.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetCatalog.WebApi.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyCollection<AccountListItem>> GetAccounts(
            [FromQuery] AccountFilter filter,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAccountsQuery(filter);
            return await sender.Send(query, cancellationToken);
        }

        [HttpGet("{accountId:int}")]
        public async Task<AccountDetails> GetAccount(
            int accountId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetAccountDetailsQuery(accountId);
            return await sender.Send(query, cancellationToken);
        }

        [HttpPost]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task<int> CreateAccount(
            AccountCreateParams createParams,
            CancellationToken cancellationToken
        )
        {
            return await sender.Send(new CreateAccountCommand(createParams), cancellationToken);
        }

        [HttpPut("{accountId:int}")]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task UpdateAccount(
            int accountId,
            AccountCreateParams AccountCreateParams,
            CancellationToken cancellationToken
        )
        {
            await sender.Send(
                new UpdateAccountCommand(accountId, AccountCreateParams),
                cancellationToken
            );
        }

        [HttpDelete("{accountId:int}")]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task DeleteAccount(int AccountId, CancellationToken cancellationToken)
        {
            await sender.Send(new DeleteAccountCommand(AccountId), cancellationToken);
        }
    }
}

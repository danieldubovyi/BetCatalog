using BetCatalog.Models.Users;
using BetCatalog.Services.BankAccounts.Commands;
using BetCatalog.Services.BankAccounts.Dto;
using BetCatalog.Services.BankAccounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetCatalog.WebApi.Controllers
{
    [ApiController]
    [Route("api/bankAccounts")]
    public class BankAccountsController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<IReadOnlyCollection<BankAccountListItem>> GetBankAccounts(
            [FromQuery] BankAccountFilter filter,
            CancellationToken cancellationToken
        )
        {
            var query = new GetBankAccountsQuery(filter);
            return await sender.Send(query, cancellationToken);
        }

        [HttpGet("{bankAccountId:int}")]
        public async Task<BankAccountDetails> GetBankAccount(
            int bankAccountId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetBankAccountDetailsQuery(bankAccountId);
            return await sender.Send(query, cancellationToken);
        }

        [HttpPost]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task<int> CreateBankAccount(
            BankAccountCreateParams createParams,
            CancellationToken cancellationToken
        )
        {
            return await sender.Send(new CreateBankAccountCommand(createParams), cancellationToken);
        }

        [HttpPut("{bankAccountId:int}")]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task UpdateBankAccount(
            int bankAccountId,
            BankAccountCreateParams bankAccountCreateParams,
            CancellationToken cancellationToken
        )
        {
            await sender.Send(
                new UpdateBankAccountCommand(bankAccountId, bankAccountCreateParams),
                cancellationToken
            );
        }

        [HttpDelete("{bankAccountId:int}")]
        //[Authorize(Roles = UserRole.Admin)]
        public async Task DeleteBankAccount(int bankAccountId, CancellationToken cancellationToken)
        {
            await sender.Send(new DeleteBankAccountCommand(bankAccountId), cancellationToken);
        }
    }
}

using BetCatalog.Models.Users;
using BetCatalog.Services.Users.Commands;
using BetCatalog.Services.Users.Dto;
using BetCatalog.Services.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BetCatalog.WebApi.Identity
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var usersGroup = endpoints.MapGroup("api/users").WithTags("Users");
            usersGroup.MapIdentityApi<User>();
            usersGroup.MapGet("/info", GetUserInfo).RequireAuthorization();
            usersGroup.MapGet("/settings", GetUserSettings).RequireAuthorization();
            usersGroup.MapPut("/settings", SetUserSettings).WithTags(nameof(SetUserSettings)).RequireAuthorization();

            return endpoints;
        }

        private async static Task<Results<Ok<UserInfo>, ValidationProblem, NotFound>> GetUserInfo(ClaimsPrincipal claimsPrincipal, IServiceProvider sp)
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            if (await userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                return TypedResults.NotFound();
            }

            var roles = await userManager.GetRolesAsync(user) ?? throw new NotSupportedException("Users must have roles.");
            var info = new UserInfo
            {
                Email = await userManager.GetEmailAsync(user) ?? throw new NotSupportedException("Users must have an email."),
                Roles = roles.ToArray()
            };

            return TypedResults.Ok(info);
        }

        private async static Task<Results<Ok<IReadOnlyCollection<UserSettingItem>>, UnauthorizedHttpResult>> GetUserSettings(
            ClaimsPrincipal claimsPrincipal,
            IServiceProvider sp,
            CancellationToken cancellationToken)
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            if (await userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                return TypedResults.Unauthorized();
            }

            var settings = await sp.GetRequiredService<ISender>().Send(new GetUserSettingsQuery(user.Id), cancellationToken);

            return TypedResults.Ok(settings);
        }

        private async static Task<Results<Ok, UnauthorizedHttpResult>> SetUserSettings(
            IReadOnlyCollection<UserSettingItem> settings,
            ClaimsPrincipal claimsPrincipal,
            IServiceProvider sp,
            CancellationToken cancellationToken)
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            if (await userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                return TypedResults.Unauthorized();
            }

            await sp.GetRequiredService<ISender>().Send(new UpdateUserSettingsCommand(user.Id, settings), cancellationToken);

            return TypedResults.Ok();
        }
    }
}

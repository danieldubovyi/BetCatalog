namespace BetCatalog.WebApi.Identity
{
    public class UserInfo
    {
        public string? Email { get; init; } = default!;
        public IReadOnlyCollection<string> Roles { get; init; } = default!;
    }
}

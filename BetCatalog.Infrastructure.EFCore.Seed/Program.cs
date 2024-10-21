using BetCatalog.Infrastructure.EFCore.Seed;

using var _ = new SeedRunner()
    .Run<AccountsSeed>()
    .Run<UsersSeed>();

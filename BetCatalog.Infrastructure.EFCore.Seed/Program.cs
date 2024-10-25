using BetCatalog.Infrastructure.EFCore.Seed;

using var _ = new SeedRunner().Run<UsersSeed>();

namespace BetCatalog.Infrastructure.EFCore.Seed
{
    internal interface ISeed
    {
        void Run(BetCatalogDbContext dbContext);
    }
}

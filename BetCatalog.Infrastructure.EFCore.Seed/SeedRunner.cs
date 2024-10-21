using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetCatalog.Infrastructure.EFCore.Seed
{
    internal class SeedRunner : IDisposable
    {
        private readonly BetCatalogDbContext dbContext;

        public SeedRunner()
        {
            dbContext = new BetCatalogDbContextFactory().CreateDbContext([]);
        }

        public SeedRunner Run<TSeed>() where TSeed : ISeed, new()
        {
            Console.Write($"Running {typeof(TSeed).Name} seed.....");
            new TSeed().Run(dbContext);
            Console.WriteLine("Done");
            return this;
        }

        public void Dispose()
        {
            dbContext?.Dispose();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("BetCatalog.Infrastructure.EFCore.Seed")]

namespace BetCatalog.Infrastructure.EFCore
{
    internal class BetCatalogDbContextFactory : IDesignTimeDbContextFactory<BetCatalogDbContext>
    {
        public BetCatalogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BetCatalogDbContext>();
            //Local MSSQL
            //optionsBuilder.UseSqlServer("data source=.;initial catalog=BetCatalog;Integrated Security=true;Encrypt=False;TrustServerCertificate=True");
            //Docker MSSQL 
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=BetCatalog;User Id=SA;Password=Q1w2_E3_r4_t5;TrustServerCertificate=True;");
            //MySql
            //optionsBuilder.UseMySQL("Server=localhost;Database=BetCatalog;Uid=root;Pwd=q1w2e3;");
            //Azure
            //optionsBuilder.UseSqlServer("Server=tcp:esportsportalwebapidbserver.database.windows.net,1433;Initial Catalog=EsportsPortal;Persist Security Info=False;User ID=epadmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new BetCatalogDbContext(optionsBuilder.Options);
        }
    }
}

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FanOfTheVan.Services.Models;

namespace FanOfTheVan.Services.Implementation.Contexts
{
    public class MarketDbContext : DbContext
    {
        public MarketDbContext() : base("FanOfTheVan")
        {

        }

        public DbSet<IMarket> Markets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

using System.Linq;
using FanOfTheVan.Services.Implementation.Contexts;
using FanOfTheVan.Services.Models;

namespace FanOfTheVan.Services.Implementation.Repositories.Implementations
{
    public class MarketRepository
    {
        private MarketDbContext context = new MarketDbContext();

        public MarketRepository()
        {

        }

        public IMarket GetMarketById(int marketId)
        {
            return context.Markets.FirstOrDefault(x => x.MarketId.Pid == marketId);
        }

        public int SaveMarket(IMarket market)
        {
            var savedMarket = context.Markets.Add(market);
            context.SaveChanges();
            return savedMarket.MarketId.Pid;
        }
    }
}

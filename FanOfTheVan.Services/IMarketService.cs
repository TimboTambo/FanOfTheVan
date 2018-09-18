using FanOfTheVan.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanOfTheVan.Services
{
    public interface IMarketService
    {
        Task<IMarket> GetMarket(string marketId);
        Task CreateMarket(Market market);
        Task<IEnumerable<IMarket>> GetAllMarkets();
        Task UpdateMarket(Market market);
    }
}

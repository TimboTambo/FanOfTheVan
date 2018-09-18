using FanOfTheVan.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FanOfTheVan.Services.Implementation.Repositories
{
    public interface IMarketRepository
    {
        Task<IMarket> GetMarketById(string marketId);
        Task SaveMarket(Market market);
        Task<IEnumerable<IMarket>> GetAllMarkets();
        Task UpdateMarket(Market market);
    }
}

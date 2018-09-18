using System.Collections.Generic;
using System.Threading.Tasks;
using FanOfTheVan.Services.Implementation.Repositories;
using FanOfTheVan.Services.Models;

namespace FanOfTheVan.Services.Implementation.Services
{
    public class MarketService : IMarketService
    {
        private readonly IMarketRepository _repository;

        public MarketService(IMarketRepository repository)
        {
            _repository = repository;
        }

        public async Task<IMarket> GetMarket(string marketId)
        {
            return await _repository.GetMarketById(marketId);
        }

        public async Task CreateMarket(Market market)
        {
            await _repository.SaveMarket(market);
        }

        public async Task<IEnumerable<IMarket>> GetAllMarkets()
        {
            return await _repository.GetAllMarkets();
        }

        public async Task UpdateMarket(Market market)
        {
            await _repository.UpdateMarket(market);
        }
    }
}

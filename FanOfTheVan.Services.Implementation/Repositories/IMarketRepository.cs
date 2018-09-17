using FanOfTheVan.Services.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace FanOfTheVan.Services.Implementation.Repositories
{
    public interface IMarketRepository
    {
        IMarket GetMarketById(string marketId);
        void SaveMarket(Market market);
        IEnumerable<IMarket> GetAllMarkets();
        void UpdateMarket(Market market);
    }
}

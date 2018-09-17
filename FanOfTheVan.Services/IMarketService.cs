using FanOfTheVan.Services.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace FanOfTheVan.Services
{
    public interface IMarketService
    {
        IMarket GetMarket(string marketId);
        void CreateMarket(Market market);
        IEnumerable<IMarket> GetAllMarkets();
        void UpdateMarket(Market market);
    }
}

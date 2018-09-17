using System;
using System.Collections.Generic;
using FanOfTheVan.Services.Implementation.Repositories;
using FanOfTheVan.Services.Models;
using MongoDB.Bson;

namespace FanOfTheVan.Services.Implementation.Services
{
    public class MarketService : IMarketService
    {
        private readonly IMarketRepository _repository;

        public MarketService(IMarketRepository repository)
        {
            _repository = repository;
        }

        public IMarket GetMarket(string marketId)
        {
            return _repository.GetMarketById(marketId);
        }

        public void CreateMarket(Market market)
        {
            _repository.SaveMarket(market);
        }

        public IEnumerable<IMarket> GetAllMarkets()
        {
            return _repository.GetAllMarkets();
        }

        public void UpdateMarket(Market market)
        {
            _repository.UpdateMarket(market);
        }
    }
}

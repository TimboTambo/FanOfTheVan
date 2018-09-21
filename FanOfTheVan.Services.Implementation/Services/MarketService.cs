using System;
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

        public async Task<IEnumerable<IMarket>> GetMarketsWithinDistance(double latitude, double longitude, int distance)
        {
            var markets = await GetAllMarkets();
            var marketsWithinDistance = new List<IMarket>();
            foreach (var market in markets)
            {
                var distanceToMarket = GetDistanceBetweenPoints(latitude, longitude, market.Latitude, market.Longitude);
                if (distanceToMarket < distance)
                {
                    marketsWithinDistance.Add(market);
                }
            }
            return marketsWithinDistance;
        }

        private double GetDistanceBetweenPoints(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            var R = 6731f;
            var latRads1 = ToRadians(latitude1);
            var latRads2 = ToRadians(latitude2);
            var diffLatRads = ToRadians(latitude2 - latitude1);
            var diffLongRads = ToRadians(longitude2 - longitude1);

            var a = Math.Sin(diffLatRads / 2) * Math.Sin(diffLatRads / 2) + Math.Cos(latRads1) * Math.Cos(latRads2) * Math.Sin(diffLongRads / 2) * Math.Sin(diffLongRads / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * 2f * (float)Math.PI / 180f;
        }
    }
}

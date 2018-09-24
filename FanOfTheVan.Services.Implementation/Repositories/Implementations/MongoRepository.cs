using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FanOfTheVan.Services.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace FanOfTheVan.Services.Implementation.Repositories.Implementations
{
    public class MongoRepository : IMarketRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public MongoRepository()
        {
            _client = new MongoClient("mongodb://127.0.0.1:27017");
            _db = _client.GetDatabase("local");
        }

        public async Task<IEnumerable<IMarket>> GetAllMarkets()
        {
            var markets = await _db.GetCollection<Market>("Markets").FindAsync(_ => true);
            return markets.ToList();
        }

        public async Task<IMarket> GetMarketById(string marketId)
        {
            var collection = _db.GetCollection<Market>("Markets");
            var id = new ObjectId(marketId);
            var market = await collection.FindAsync(x => x.MarketId == id);
            return market.FirstOrDefault();
        }

        public async Task SaveMarket(Market market)
        {
            var collection = _db.GetCollection<Market>("Markets");
            await collection.InsertOneAsync(market);
        }

        public async Task UpdateMarket(Market market)
        {
            var collection = _db.GetCollection<Market>("Markets");
            var filter = Builders<Market>.Filter.Eq(x => x.MarketId, market.MarketId);
            await collection.ReplaceOneAsync(filter, market);
        }
    }
}

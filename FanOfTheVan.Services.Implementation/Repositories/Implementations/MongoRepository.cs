using System;
using System.Collections.Generic;
using FanOfTheVan.Services.Models;
using MongoDB.Bson;
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

        public IEnumerable<IMarket> GetAllMarkets()
        {
            return _db.GetCollection<Market>("Markets").Find(_ => true).ToList();
        }

        public IMarket GetMarketById(string marketId)
        {
            var collection = _db.GetCollection<Market>("Markets");
            var id = new ObjectId(marketId);
            return collection.Find(x => x.MarketId == id).FirstOrDefault();
        }

        public void SaveMarket(Market market)
        {
            var collection = _db.GetCollection<Market>("Markets");
            collection.InsertOne(market);
        }

        public void UpdateMarket(Market market)
        {
            var collection = _db.GetCollection<Market>("Markets");
            var filter = Builders<Market>.Filter.Eq(x => x.MarketId, market.MarketId);
            collection.ReplaceOne(filter, market);
        }
    }
}

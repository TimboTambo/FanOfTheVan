using FanOfTheVan.Services.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace FanOfTheVan.Services.Models
{
    public class Market : IMarket
    {
        [BsonId]
        public ObjectId MarketId { get; set; }
        public string Name { get; set; }
        public int PhotoId { get; set; }
        public MarketOpenRule MondayOpen { get; set; }
        public MarketOpenRule TuesdayOpen { get; set; }
        public MarketOpenRule WednesdayOpen { get; set; }
        public MarketOpenRule ThursdayOpen { get; set; }
        public MarketOpenRule FridayOpen { get; set; }
        public MarketOpenRule SaturdayOpen { get; set; }
        public MarketOpenRule SundayOpen { get; set; }
        public List<MarketTag> Tags { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public Guid ContactUserId { get; set; }
        public string StreetAddress { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
    }
}

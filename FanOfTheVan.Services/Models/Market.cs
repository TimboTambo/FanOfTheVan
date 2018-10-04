using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;

namespace FanOfTheVan.Services.Models
{
    public class Market : IMarket
    {
        public Market()
        {
            OpeningTimes = new Dictionary<DayOfWeek, MarketOpenRule> {
            { DayOfWeek.Monday, new MarketOpenRule()},
            { DayOfWeek.Tuesday, new MarketOpenRule()},
            { DayOfWeek.Wednesday, new MarketOpenRule()},
            { DayOfWeek.Thursday, new MarketOpenRule()},
            { DayOfWeek.Friday, new MarketOpenRule()},
            { DayOfWeek.Saturday, new MarketOpenRule()},
            { DayOfWeek.Sunday, new MarketOpenRule()}};
        }

        [BsonId]
        public ObjectId MarketId { get; set; }
        public string Name { get; set; }
        public int PhotoId { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<DayOfWeek, MarketOpenRule> OpeningTimes { get; set; }
        public List<MarketTag> Tags { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public Guid ContactUserId { get; set; }
        public string StreetAddress { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        [BsonIgnore]
        public double Distance { get; set; }
        public string Url { get; set; }
        [BsonIgnore]
        public string Colour { get; set; }
        [BsonIgnore]
        public DayOfWeek? ResultDayOfWeek { get; set; }
        [BsonIgnore]
        public MarketOpenRule ResultOpeningTimes { get; set; }
    }
}

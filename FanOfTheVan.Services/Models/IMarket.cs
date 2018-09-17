using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace FanOfTheVan.Services.Models
{
    public interface IMarket
    {
        ObjectId MarketId { get; set; }
        string Name { get; set; }
        int PhotoId { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        string Description { get; set; }
        Guid ContactUserId { get; set; }
        MarketOpenRule MondayOpen { get; set; }
        MarketOpenRule TuesdayOpen { get; set; }
        MarketOpenRule WednesdayOpen { get; set; }
        MarketOpenRule ThursdayOpen { get; set; }
        MarketOpenRule FridayOpen { get; set; }
        MarketOpenRule SaturdayOpen { get; set; }
        MarketOpenRule SundayOpen { get; set; }
        List<MarketTag> Tags { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FanOfTheVan.Services.Models
{
    public class Market
    {
        public int MarketId { get; set; }
        public string Name { get; set; }
        public int PhotoId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public Guid ContactUserId { get; set; }
        public MarketOpenRule MondayOpen { get; set; }
        public MarketOpenRule TuesdayOpen { get; set; }
        public MarketOpenRule WednesdayOpen { get; set; }
        public MarketOpenRule ThursdayOpen { get; set; }
        public MarketOpenRule FridayOpen { get; set; }
        public MarketOpenRule SaturdayOpen { get; set; }
        public MarketOpenRule SundayOpen { get; set; }
    }
}

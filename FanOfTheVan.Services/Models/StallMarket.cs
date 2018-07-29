using System;

namespace FanOfTheVan.Services.Models
{
    public class StallMarket
    {
        public int StallMarketId { get; set; }
        public int MarketId { get; set; }
        public int StallId { get; set; }
        // For if the stall is at the market one day only
        // Need to add more sophisticated logic in case the stall is there every Tuesday only, for example
        public DateTime? Date { get; set; }
    }
}

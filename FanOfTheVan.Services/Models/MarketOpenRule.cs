using System;

namespace FanOfTheVan.Services.Models
{
    public class MarketOpenRule
    {
        public int MarketOpenRuleId { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public RepeatRule RepeatRule { get; set; }
    }
}

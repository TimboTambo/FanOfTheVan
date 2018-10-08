using System.Collections.Generic;

namespace FanOfTheVan.Services.Models
{
    public class MarketsViewModel
    {
        public MarketsViewModel()
        {
            Markets = new List<IMarket>();
        }

        public List<IMarket> Markets { get; set; }
        public string SearchTerm { get; set; }
        public OpenStatus OpenStatus { get; set; }
        public int Distance { get; set; }
    }
}

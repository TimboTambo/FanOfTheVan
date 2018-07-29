using System.Collections.Generic;

namespace FanOfTheVan.Services.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public int StallId { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}

namespace FanOfTheVan.Services.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public int StallId { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }
        public int PrimaryPhotoId { get; set; }
        public DishType? Type { get; set; }
    }

    public enum DishType
    {
        Starter = 0,
        Main = 1,
        Dessert = 2,
        Side = 3,
        Snack = 4,
        SoftDrink = 5,
        AlcoholicDrink = 6
    }
}

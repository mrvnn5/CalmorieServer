using TCoYB.Server.Models;

namespace TCoYB.Server.Repository
{
    public interface IRepository
    {
        public AppUser? GetUser(string username);
        public void CreateUser(AppUser user);
        public void UpdateUser(AppUser user);
        public void DeleteUser(string username);
        /*public FoodItem? GetFoodItem(Guid id);
        public void AddFoodItem (FoodItem foodItem);
        public void DeleteFoodItem(Guid id);*/
        /*public List<FoodItem> GetFoodItems(AppUser user);
        public List<FoodItem> GetFoodItems(AppUser user, DateTime date);
        public List<FoodItem> GetFoodItems(AppUser user, DateTime date, MealType mealType);*/
    }
}

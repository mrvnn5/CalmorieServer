using Microsoft.EntityFrameworkCore;
using TCoYB.Server.Models;

namespace TCoYB.Server.Repository
{
    public class Repository : IRepository
    {
        private ApplicationContext Context { get; set; }
        public Repository(ApplicationContext applicationContext)
        {
            Context = applicationContext;
        }
        /*public void AddFoodItem(FoodItem foodItem)
        {
            Context.FoodItems.Add(foodItem);
            Context.SaveChanges();
        }*/

        public void CreateUser(AppUser user)
        {
            Context.AppUsers.Add(user);
            Context.SaveChanges();
        }

        /*public void DeleteFoodItem(Guid id)
        {
            FoodItem? toDelete = Context.FoodItems.FirstOrDefault(f => f.Id == id);
            if (toDelete != null)
            {
                Context.FoodItems.Remove(toDelete);
                Context.SaveChanges();
            }
        }*/

        public void DeleteUser(string username)
        {
            AppUser? toDelete = Context.AppUsers.FirstOrDefault(u => u.Username == username);
            if (toDelete != null)
            {
                Context.AppUsers.Remove(toDelete);
                Context.SaveChanges();
            }
        }

        /*public FoodItem? GetFoodItem(Guid id)
        {
            return Context.FoodItems.FirstOrDefault(f => f.Id == id);
        }*/



        /*public List<FoodItem> GetFoodItems(AppUser user)
        {
            return Context.FoodItems.Where(f => f.User == user).ToList();
        }

        public List<FoodItem> GetFoodItems(AppUser user, DateTime date)
        {
            return Context.FoodItems.Where(f => f.User == user && f.Date == date).ToList();
        }

        public List<FoodItem> GetFoodItems(AppUser user, DateTime date, MealType mealType)
        {
            return Context.FoodItems.Where(f => f.User == user && f.Date == date && f.MealType == mealType).ToList();
        }*/

        public AppUser? GetUser(string username)
        {
            return Context.AppUsers.Include(u => u.FoodItems).Include(u => u.UserToken).Include(u => u.WaterItems).FirstOrDefault(u => u.Username == username);
        }

        public void UpdateUser(AppUser user)
        {
            AppUser? toUpdate = Context.AppUsers.Include(u => u.FoodItems).Include(u => u.UserToken).Include(u => u.WaterItems).FirstOrDefault(u => u.Username == user.Username);
            if(toUpdate != null)
            {
                toUpdate.UserToken = user.UserToken;
                toUpdate.Update(user);
                toUpdate.FoodItems.RemoveAll(f => !user.FoodItems.Contains(f));
                user.FoodItems.ForEach(f => { if (!toUpdate.FoodItems.Contains(f)) { f.Id = new Guid(); toUpdate.FoodItems.Add(f); } });

                toUpdate.WaterItems.RemoveAll(w => !user.WaterItems.Contains(w));
                user.WaterItems.ForEach(w => { if (!toUpdate.WaterItems.Contains(w)) { w.Id = new Guid(); toUpdate.WaterItems.Add(w); } else toUpdate.WaterItems.Find(x => x.Id == w.Id).Volume = w.Volume; });

                //Context.AppUsers.Update(toUpdate);

                Context.SaveChanges();
            }
        }
    }
}

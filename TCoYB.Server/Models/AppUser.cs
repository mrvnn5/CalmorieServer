using System.ComponentModel.DataAnnotations;

namespace TCoYB.Server.Models
{
    public class AppUser
    {
        [Key]
        [Required]
        public string Username { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public Sex Sex { get; set; }
        [Required]
        public Activity Activity { get; set; }
        [Required]
        public Plan Plan { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string? PasswordHash { get; set; }
        public UserToken? UserToken { get; set; }
        public List<FoodItem>? FoodItems { get; set; } = new List<FoodItem>();
        public List<WaterItem>? WaterItems { get; set; } = new List<WaterItem>();

        public AppUser(string username, int height, int weight, Sex sex, Activity activity, Plan plan, DateTime birthDate, string passwordHash)
        {
            Username = username;
            Height = height;
            Weight = weight;
            Sex = sex;
            Activity = activity;
            Plan = plan;
            BirthDate = birthDate;
            PasswordHash = passwordHash;
        }

        public void Update(AppUser newUser)
        {
            Height = newUser.Height;
            Weight = newUser.Weight;
            Sex = newUser.Sex;
            Activity = newUser.Activity;
            Plan = newUser.Plan;
            BirthDate = newUser.BirthDate;
            //FoodItems = new List<FoodItem>(newUser.FoodItems);
        }

        public AppUser GetUserForApp()
        {
            AppUser user = (AppUser)this.MemberwiseClone();
            user.PasswordHash = "";
            return user;
        }
    }
    public enum Sex
    {
        Male = 0,
        Female = 1
    }
    public enum Activity
    {
        Low = 0,
        Middle = 1,
        High = 2,
        VeryHigh = 3
    }
    public enum Plan
    {
        Loss = 0,
        Support = 1,
        Gain = 2
    }
}

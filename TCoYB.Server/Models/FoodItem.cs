using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCoYB.Server.Models
{
    public class FoodItem
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public MealType MealType { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return Id == (obj as FoodItem).Id;
        }
    }

    public enum MealType
    {
        Breakfast = 0,
        Lunch = 1,
        Dinner = 2,
        Snack = 3
    }
}

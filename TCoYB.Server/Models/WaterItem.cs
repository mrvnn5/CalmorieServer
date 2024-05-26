using Microsoft.Build.Framework;

namespace TCoYB.Server.Models
{
    public class WaterItem
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Username { get; set; }
      
        [Required]
        public int Volume { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return Id == (obj as WaterItem).Id;
        }
    }
}

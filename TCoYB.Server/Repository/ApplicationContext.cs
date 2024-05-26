using Microsoft.EntityFrameworkCore;
using TCoYB.Server.Models;

namespace TCoYB.Server.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        /*public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }*/
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.FoodItems)
                .WithOne()
                .HasForeignKey(f => f.Username);

            modelBuilder.Entity<AppUser>()
               .HasMany(u => u.WaterItems)
               .WithOne()
               .HasForeignKey(w => w.Username);

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.UserToken)
                .WithOne()
                .HasForeignKey<UserToken>(t => t.Username);
        }
    }
}

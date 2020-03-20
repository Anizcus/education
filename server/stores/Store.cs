using Microsoft.EntityFrameworkCore;
using Server.Stores.Entities;

namespace Server.Stores
{
   public class Store : DbContext
   {
      public Store(DbContextOptions<Store> options) : base(options) { }

      public DbSet<User> Users { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<User>().ToTable("user");
      }
   }
}
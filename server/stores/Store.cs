using Microsoft.EntityFrameworkCore;
using Server.Stores.Configurations;
using Server.Stores.Entities;

namespace Server.Stores
{
   public class Store : DbContext
   {
      #region properties
      public DbSet<User> Users { get; set; }
      public DbSet<Role> Roles { get; set; }
      public DbSet<Permission> Permissions { get; set; }
      #endregion
      public Store(DbContextOptions options) : base(options) { }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfiguration(new UserConfiguration());
         modelBuilder.ApplyConfiguration(new RoleConfiguration());
         modelBuilder.ApplyConfiguration(new PermissionConfiguration());
         modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
      }
   }
}
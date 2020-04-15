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
      public DbSet<Category> Categories { get; set; }
      public DbSet<Type> Types { get; set; }
      public DbSet<Lesson> Lessons { get; set; }
      public DbSet<State> States { get; set; }
      public DbSet<Assignment> Assignments { get; set; }
      public DbSet<Progress> Progresses { get; set; }
      #endregion
      public Store(DbContextOptions options) : base(options) { }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfiguration(new UserConfiguration());
         modelBuilder.ApplyConfiguration(new RoleConfiguration());
         modelBuilder.ApplyConfiguration(new PermissionConfiguration());
         modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
         modelBuilder.ApplyConfiguration(new CategoryConfiguration());
         modelBuilder.ApplyConfiguration(new TypeConfiguration());
         modelBuilder.ApplyConfiguration(new StateConfiguration());
         modelBuilder.ApplyConfiguration(new ProgressConfiguration());
         modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
         modelBuilder.ApplyConfiguration(new LessonConfiguration());
         modelBuilder.ApplyConfiguration(new UserLessonConfiguration());
         modelBuilder.ApplyConfiguration(new UserAssignmentConfiguration());
      }
   }
}
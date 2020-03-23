using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Enums;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
   {
      public void Configure(EntityTypeBuilder<Permission> builder)
      {
         builder.ToTable("permission");

         builder.HasKey(e => e.Id);

         builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(32)")
            .IsRequired();

         builder.Property(e => e.Created)
            .HasColumnName("create_time")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

         builder.Property(e => e.Updated)
            .HasColumnName("update_time")
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP")
            .ValueGeneratedOnUpdate();

         var user = nameof(PermissionEnum.User);
         var lesson = nameof(PermissionEnum.Lesson);

         builder.HasData(
            new Permission { Name = $"{ user }.{ nameof(PermissionEnum.User.View) }", Id = (uint) PermissionEnum.User.View },
            new Permission { Name = $"{ user }.{ nameof(PermissionEnum.User.Create) }", Id = (uint) PermissionEnum.User.Create },
            new Permission { Name = $"{ user }.{ nameof(PermissionEnum.User.Update) }", Id = (uint) PermissionEnum.User.Update },
            new Permission { Name = $"{ user }.{ nameof(PermissionEnum.User.Delete) }", Id = (uint) PermissionEnum.User.Delete },
            new Permission { Name = $"{ lesson }.{ nameof(PermissionEnum.Lesson.View) }", Id = (uint) PermissionEnum.Lesson.View },
            new Permission { Name = $"{ lesson }.{ nameof(PermissionEnum.Lesson.Create) }", Id = (uint) PermissionEnum.Lesson.Create },
            new Permission { Name = $"{ lesson }.{ nameof(PermissionEnum.Lesson.Update) }", Id = (uint) PermissionEnum.Lesson.Update },
            new Permission { Name = $"{ lesson }.{ nameof(PermissionEnum.Lesson.Delete) }", Id = (uint) PermissionEnum.Lesson.Delete }
         );
      }
   }
}
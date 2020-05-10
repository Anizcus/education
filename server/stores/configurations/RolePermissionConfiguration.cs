using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Enums;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
   {
      public void Configure(EntityTypeBuilder<RolePermission> builder)
      {
         builder.ToTable("role_permission");

         builder
            .HasKey(e => new { e.RoleId, e.PermissionId })
            .HasName("INDEX_ROLE_PERMISSION_ID");

         builder
            .HasIndex(e => e.RoleId)
            .HasName("INDEX_ROLE_PERMISSION_ROLE_ID");

         builder
            .HasIndex(e => e.PermissionId)
            .HasName("INDEX_ROLE_PERMISSION_PERMISSION_ID");

         builder.Property(e => e.RoleId)
            .HasColumnName("role_id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.PermissionId)
            .HasColumnName("permission_id")
            .HasColumnType("int(10) unsigned");

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

         builder
            .HasOne(e => e.Permission)
            .WithMany(r => r.PermissionRoles)
            .HasForeignKey(e => e.PermissionId)
            .HasConstraintName("FOREIGN_ROLE_PERMISSION_PERMISSION_ID");

         builder
            .HasOne(e => e.Role)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(e => e.RoleId)
            .HasConstraintName("FOREIGN_ROLE_PERMISSION_ROLE_ID");

         builder.HasData(
            new RolePermission { RoleId = (uint)RoleEnum.Student, PermissionId = (uint)PermissionEnum.User.View },
            new RolePermission { RoleId = (uint)RoleEnum.Student, PermissionId = (uint)PermissionEnum.User.Create },
            new RolePermission { RoleId = (uint)RoleEnum.Student, PermissionId = (uint)PermissionEnum.User.Update },
            new RolePermission { RoleId = (uint)RoleEnum.Student, PermissionId = (uint)PermissionEnum.User.Delete },
            new RolePermission { RoleId = (uint)RoleEnum.Administrator, PermissionId = (uint)PermissionEnum.Lesson.Authorize },
            new RolePermission { RoleId = (uint)RoleEnum.Administrator, PermissionId = (uint)PermissionEnum.User.Modify },
            new RolePermission { RoleId = (uint)RoleEnum.Teacher, PermissionId = (uint)PermissionEnum.Lesson.Update },
            new RolePermission { RoleId = (uint)RoleEnum.Teacher, PermissionId = (uint)PermissionEnum.Lesson.Create }
         );
      }
   }
}
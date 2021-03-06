using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Enums;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class RoleConfiguration : IEntityTypeConfiguration<Role>
   {
      public void Configure(EntityTypeBuilder<Role> builder)
      {
         builder.ToTable("role");

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

         builder.HasData(
            new Role { Name = nameof(RoleEnum.Student), Id = (uint) RoleEnum.Student },
            new Role { Name = nameof(RoleEnum.Teacher), Id = (uint) RoleEnum.Teacher },
            new Role { Name = nameof(RoleEnum.Administrator), Id = (uint) RoleEnum.Administrator }
         );
      }
   }
}
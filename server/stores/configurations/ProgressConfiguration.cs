using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Enums;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
   {
      public void Configure(EntityTypeBuilder<Progress> builder)
      {
         builder.ToTable("progress");

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
            new Progress { Name = nameof(ProgressEnum.Active), Id = (uint) ProgressEnum.Active },
            new Progress { Name = nameof(ProgressEnum.Paused), Id = (uint) ProgressEnum.Paused },
            new Progress { Name = nameof(ProgressEnum.Completed), Id = (uint) ProgressEnum.Completed }
         );
      }
   }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class StatusConfiguration : IEntityTypeConfiguration<Status>
   {
      public void Configure(EntityTypeBuilder<Status> builder)
      {
         builder.ToTable("status");

         builder.HasKey(e => e.Id);

         builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.Description)
            .HasColumnName("description")
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

         builder.Property(e => e.StateId)
            .HasColumnName("state_id")
            .HasColumnType("int(10) unsigned");

         builder.HasIndex(e => e.StateId).HasName("INDEX_STATUS_STATE_ID");

         builder
            .HasOne(u => u.State)
               .WithMany(r => r.Statuses)
            .HasForeignKey(u => u.StateId)
            .HasConstraintName("FOREIGN_STATUS_STATE_ID");
      }
   }
}

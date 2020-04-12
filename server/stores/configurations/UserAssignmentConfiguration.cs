using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class UserAssignmentConfiguration : IEntityTypeConfiguration<UserAssignment>
   {
      public void Configure(EntityTypeBuilder<UserAssignment> builder)
      {
         builder.ToTable("user_assignment");

         builder
            .HasKey(e => new { e.UserId, e.AssignmentId })
            .HasName("INDEX_USER_ASSIGNMENT_ID");

         builder
            .HasIndex(e => e.UserId)
            .HasName("INDEX_USER_ASSIGNMENT_USER_ID");

         builder
            .HasIndex(e => e.AssignmentId)
            .HasName("INDEX_USER_ASSIGNMENT_ASSIGNMENT_ID");

         builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.AssignmentId)
            .HasColumnName("assignment_id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.ProgressId)
            .HasColumnName("progress_id")
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
            .HasOne(e => e.Assignment)
            .WithMany(r => r.AssignmentUsers)
            .HasForeignKey(e => e.AssignmentId)
            .HasConstraintName("FOREIGN_USER_ASSIGNMENT_ASSIGNMENT_ID");

         builder
            .HasOne(e => e.User)
            .WithMany(p => p.UserAssignments)
            .HasForeignKey(e => e.UserId)
            .HasConstraintName("FOREIGN_USER_ASSIGNMENT_USER_ID");

         builder.HasIndex(e => e.ProgressId).HasName("INDEX_USER_ASSIGNMENT_PROGRESS_ID");

         builder
            .HasOne(u => u.Progress)
               .WithMany(r => r.UserAssignments)
            .HasForeignKey(u => u.ProgressId)
            .HasConstraintName("FOREIGN_USER_ASSIGNMENT_PROGRESS_ID");
      }
   }
}
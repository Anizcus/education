using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Stores.Entities;

namespace Server.Stores.Configurations
{
   public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
   {
      public void Configure(EntityTypeBuilder<Assignment> builder)
      {
         builder.ToTable("assignment");

         builder.HasKey(e => e.Id);

         builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.Experience)
            .HasColumnName("experience")
            .HasColumnType("int(10) unsigned");

         builder.Property(e => e.Description)
            .HasColumnName("description")
            .HasColumnType("varchar(256)")
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

         builder.Property(e => e.LessonId)
            .HasColumnName("lesson_id")
            .HasColumnType("int(10) unsigned");

         builder.HasIndex(e => e.LessonId).HasName("INDEX_ASSIGNMENT_LESSON_ID");

         builder
            .HasOne(u => u.Lesson)
               .WithMany(r => r.Assignments)
            .HasForeignKey(u => u.LessonId)
            .HasConstraintName("FOREIGN_ASSIGNMENT_LESSON_ID");
      }
   }
}
